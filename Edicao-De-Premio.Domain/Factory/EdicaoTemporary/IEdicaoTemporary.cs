using Domain.Models;

namespace Domain.Factory;


public interface IEdicaoTemporaryFactory
{
    EdicaoTemporary Create(Guid userId, DateOnly date, string descricao);

    public EdicaoTemporary Create(IEdicaoTemporaryVisitor visitor);

    
}