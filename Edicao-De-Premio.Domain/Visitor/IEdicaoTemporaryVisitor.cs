

public interface IEdicaoTemporaryVisitor
{
    Guid Id { get; }

    Guid UserId { get; }

    DateOnly Date { get; }

    string Descricao { get; }


}