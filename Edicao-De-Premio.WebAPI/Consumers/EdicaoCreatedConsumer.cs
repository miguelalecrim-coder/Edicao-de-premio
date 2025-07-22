

using Application.Interfaces;
using Domain.Interfaces;
using Domain.Messages;
using MassTransit;

namespace WebApi.Consumers;

public class EdicaoCreatedConsumer : IConsumer<EdicaoCreatedMessage>
{

    private readonly IEdicaoService _edicaoService;

    public EdicaoCreatedConsumer(IEdicaoService edicaoService)
    {
        _edicaoService = edicaoService;
    }
    public async Task Consume(ConsumeContext<EdicaoCreatedMessage> context)
    {
        await _edicaoService.AddEdicaoReferenceAsync(context.Message.Id, context.Message.UserId, context.Message.Date, context.Message.TipoId);
    }
}