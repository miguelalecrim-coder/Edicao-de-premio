using Application.Messages.Edicao;
using Domain.Interfaces;

namespace Application.Interfaces;


public interface IEdicaoTemporaryService
{
    Task CreateEdicaoTemporaryAsync(CreateEdicaoRequestMessage createEdicaoRequestMessage);

    Task<IEdicaoTemporary> GetByDescricaoAsync(string descricao);

    Task DeleteEdicaoTemporaryAsyn(Guid id);

    Task PublishCreateReqEdicaoSaga(CreatedEdicaoTipoDTO dto);
}