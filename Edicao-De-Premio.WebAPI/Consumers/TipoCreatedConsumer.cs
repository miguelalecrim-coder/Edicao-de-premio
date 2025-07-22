using Application.Services;
using Domain.Messages;
using MassTransit;

namespace WebApi.Consumers;

public class TipoCreatedConsumer : IConsumer<TipoCreatedMessage>
{
    private readonly TipoService _tipoService;


    public TipoCreatedConsumer(TipoService tipoService)
    {
        _tipoService = tipoService;
    }

    public async Task Consume(ConsumeContext<TipoCreatedMessage> context)
    {
        var tipoId = context.Message.Id;
        await _tipoService.AddTipoReferenceAsync(tipoId);
    }

    
}