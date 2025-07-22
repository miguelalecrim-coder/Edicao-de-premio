


using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITipoFactory
{
    public Task<Tipo> Create(Guid id);

    public Tipo Create(ITipoVisitor tipoVisitor);
}