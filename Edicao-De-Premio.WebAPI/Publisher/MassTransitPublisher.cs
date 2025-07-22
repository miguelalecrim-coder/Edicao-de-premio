using Application.IPublishers;
using Application.Messages.Edicao;
using Domain.Interfaces;
using Domain.Messages;
using MassTransit;

namespace WebApi.Publishers;

public class MassTransitPublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public MassTransitPublisher(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task PublishEdicaoCreatedAsync(IEdicao edicao)
    {
        var eventMessage = new EdicaoCreatedMessage(
            edicao.Id,
            edicao.UserId,
            edicao.Date,
            edicao.TipoId
        );

        await _publishEndpoint.Publish(eventMessage);
    }

    public async Task PublishSagaAsync(CreateEdicaoRequestMessage message)
    {
         var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:edicao-saga-queue-{InstanceInfo.InstanceId}"));
            await endpoint.Send(message);
    }
}