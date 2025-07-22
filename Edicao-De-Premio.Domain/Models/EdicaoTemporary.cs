using Domain.Interfaces;

namespace Domain.Models;


public class EdicaoTemporary : IEdicaoTemporary
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public DateOnly Date { get; }
    public string Descricao { get; }

    public EdicaoTemporary(Guid userId, DateOnly date, string descricao)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Date = date;
        Descricao = descricao;
    }

    public EdicaoTemporary(Guid id, Guid userId, DateOnly date, string descricao)
    {
        Id = id;
        UserId = userId;
        Date = date;
        Descricao = descricao;
    }


}