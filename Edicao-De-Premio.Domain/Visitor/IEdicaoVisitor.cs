
namespace Domain.Visitor;

public interface IEdicaoVisitor
{
    Guid Id { get; }

    Guid UserId { get; }

    DateOnly Date { get; }

    Guid TipoId { get; }
}