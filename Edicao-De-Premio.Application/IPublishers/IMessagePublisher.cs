using Application.Messages.Edicao;
using Domain.Interfaces;

namespace Application.IPublishers;

public interface IMessagePublisher
{
    Task PublishEdicaoCreatedAsync(IEdicao edicao);
    Task PublishSagaAsync(CreateEdicaoRequestMessage message);    
}