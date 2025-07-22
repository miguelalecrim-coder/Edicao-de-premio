namespace Domain.Interfaces;

public interface IEdicao
{
    public Guid Id { get; }
    public Guid UserId { get; }

    public DateOnly Date { get; }

    public Guid TipoId { get; }

    
}