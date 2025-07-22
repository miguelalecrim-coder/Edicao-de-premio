
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;


public interface IEdicaoFactory
{
    Task<Edicao> Create(Guid userId, DateOnly date, Guid tipoId);
    Edicao Create(Guid edicaoId, Guid userId, DateOnly date, Guid tipoId);

    Edicao Create(IEdicaoVisitor edicaoVisitor);

    Edicao ConvertFromTemporary(IEdicaoTemporary edicaoTemporary, Guid tipoId);

    
}