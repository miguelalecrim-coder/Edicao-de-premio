using Domain.Models;

namespace Domain.Factory;

public class EdicaoTemporaryFactory : IEdicaoTemporaryFactory
{
    public EdicaoTemporary Create(Guid userId, DateOnly date, string descricao)
    {
        return new EdicaoTemporary(userId, date, descricao);
    }

    public EdicaoTemporary Create(IEdicaoTemporaryVisitor visitor)
    {
        return new EdicaoTemporary(visitor.Id, visitor.UserId, visitor.Date, visitor.Descricao);
    }
}