using Application.Interfaces;
using Application.IPublishers;
using Application.Messages.Edicao;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services;


public class EdicaoTemporaryService : IEdicaoTemporaryService
{
    private readonly IEdicaoTemporaryRepository _repository;
    private readonly IEdicaoTemporaryFactory _factory;
    private readonly IMessagePublisher _publisher;

    public EdicaoTemporaryService(IEdicaoTemporaryRepository edicaoTemporaryRepository, IEdicaoTemporaryFactory factory, IMessagePublisher publisher)
    {
        _repository = edicaoTemporaryRepository;
        _factory = factory;
        _publisher = publisher;
    }

    public async Task CreateEdicaoTemporaryAsync(CreateEdicaoRequestMessage createEdicaoRequestMessage)
    {
        var edicaoTemp = _factory.Create(createEdicaoRequestMessage.UserId, createEdicaoRequestMessage.Date, createEdicaoRequestMessage.Descricao);
        await _repository.AddAsync(edicaoTemp);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteEdicaoTemporaryAsyn(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id)
        ?? throw new InvalidOperationException("Edicao not found with the ID.");
        await _repository.DeleteAsync(id);
            

            await _repository.SaveChangesAsync();
    }

    public async Task<IEdicaoTemporary> GetByDescricaoAsync(string descricao)
    {
        return await _repository.GetByDescricaoAsync(descricao)
        ?? throw new InvalidOperationException("Edicao not found with the provided Descricao.");
    }

    public async Task PublishCreateReqEdicaoSaga(CreatedEdicaoTipoDTO dto)
    {
        var message = new CreateEdicaoRequestMessage(dto.UserId, dto.Date, dto.Descricao);
        await _publisher.PublishSagaAsync(message);
    }
}
