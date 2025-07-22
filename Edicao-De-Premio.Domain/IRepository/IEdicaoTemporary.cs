


using Domain.Interfaces;
using Domain.Models;

namespace Domain.IRepository;

public interface IEdicaoTemporaryRepository : IGenericRepositoryEF<IEdicaoTemporary, EdicaoTemporary, IEdicaoTemporaryVisitor>
{
    Task DeleteAsync(Guid id);

    Task<IEdicaoTemporary?> GetByDescricaoAsync(string descricao); 
}
