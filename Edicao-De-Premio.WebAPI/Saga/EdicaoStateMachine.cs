using Application.Interfaces;
using Application.Messages.Edicao;
using Domain.Factory;
using Domain.Messages;
using MassTransit;


public class EdicaoStateMachine : MassTransitStateMachine<EdicaoState>
{
    public State WaitingForTipo { get; private set; } = default!;

    public State CurrentState { get; private set; } = default!;

    public State Completed { get; private set; } = default!;

    public Event<CreateEdicaoRequestMessage> CreateEdicaoRequested { get; private set; } = default!;

    public Event<TipoCreatedMessage> TipoCreated { get; private set; } = default!;


    public EdicaoStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreateEdicaoRequested, x =>
        {
            x.CorrelateBy((saga, context) => saga.Descricao == context.Message.Descricao);
            x.SelectId(context => NewId.NextGuid());
            x.SetSagaFactory(context => new EdicaoState
            {
                Descricao = context.Message.Descricao,
            });
        });
        Event(() => TipoCreated, x =>
        {
            x.CorrelateBy((saga, context) => saga.Descricao == context.Message.Descricao);
            x.OnMissingInstance(m => m.Execute(context =>
            {
                Console.WriteLine($"TipoCreatedMessage recebido para descricao {context.Message.Descricao} sem saga correspondente");
            }));
        });
        Initially(
            When(CreateEdicaoRequested)
            .ThenAsync(async ctx =>
            {
                var provider = ctx.GetPayload<IServiceProvider>();
                using var scope = provider.CreateScope();

                var edicaoTemporaryService = scope.ServiceProvider.GetRequiredService<IEdicaoTemporaryService>();

                await edicaoTemporaryService.CreateEdicaoTemporaryAsync(ctx.Message);
            }).Then(ctx =>
            {
                ctx.Saga.Descricao = ctx.Message.Descricao;
                Console.WriteLine($"Saga criada com Descricao: {ctx.Saga.Descricao}");
            })
            .Send(new Uri("queue:tipos-cmd-saga"), ctx => new EdicaoWithoutTipoCreatedMessage(
                
                ctx.Message.Descricao
            ))
            .TransitionTo(WaitingForTipo)
        );

        During(WaitingForTipo,
        When(CreateEdicaoRequested)
        .Then(ctx =>
        {
            Console.WriteLine($" Pedido CreateEdicaoRequested ignorado para descricao: {ctx.Message.Descricao}");
        }),
        When(TipoCreated)
        .ThenAsync(async ctx =>
        {
            var provider = ctx.GetPayload<IServiceProvider>();
            using var scope = provider.CreateScope();

            var edicaoTemporaryService = scope.ServiceProvider.GetRequiredService<IEdicaoTemporaryService>();
            var edicaoFactory = scope.ServiceProvider.GetRequiredService<IEdicaoFactory>();
            var edicaoService = scope.ServiceProvider.GetRequiredService<IEdicaoService>();

            var temp = await edicaoTemporaryService.GetByDescricaoAsync(ctx.Message.Descricao);
            if (temp is null)
                throw new InvalidOperationException("EdicaoTemp not found.");
            var edicao = edicaoFactory.ConvertFromTemporary(temp, ctx.Message.Id);

            await edicaoService.CreateEdicaoAsync(edicao);
            await edicaoTemporaryService.DeleteEdicaoTemporaryAsyn(temp.Id);
            await ctx.Publish(new EdicaoCreatedMessage(
                edicao.Id,
                edicao.UserId,
                edicao.Date,
                edicao.TipoId
                ));
        })
        .TransitionTo(Completed)
        .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}