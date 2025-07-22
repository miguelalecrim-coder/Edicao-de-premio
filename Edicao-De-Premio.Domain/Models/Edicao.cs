using Domain.Interfaces;


namespace Domain.Models;


public class Edicao : IEdicao
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public DateOnly Date { get; }

    public Guid TipoId { get; }


    public Edicao(Guid userId, DateOnly date, Guid tipoId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Date = date;
        TipoId = tipoId;
    }


    public Edicao(Guid id, Guid userId, DateOnly date, Guid tipoId)
    {
        Id = id;
        UserId = userId;
        Date = date;
        TipoId = tipoId;
    }
}