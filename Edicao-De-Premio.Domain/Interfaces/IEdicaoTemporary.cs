namespace Domain.Interfaces;

public interface IEdicaoTemporary
{
    Guid Id { get; }

    Guid UserId { get; }

    DateOnly Date { get; }

    string Descricao { get; }
}