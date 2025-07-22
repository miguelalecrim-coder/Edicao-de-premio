using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ITipoRepository : IGenericRepositoryEF<ITipo, Tipo, ITipoVisitor>
{
    Task<bool> Exists(Guid ID);
}