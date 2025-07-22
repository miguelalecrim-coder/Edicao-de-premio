using Domain.Models;

namespace Application.DTO.Edicao;

public record EdicaoDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }

    public Guid TipoId { get; set; }


    public EdicaoDTO()
    { }

    public EdicaoDTO(Guid id, Guid userId, DateOnly date, Guid tipoId)
    {
        Id = id;
        UserId = userId;
        Date = date;
        TipoId = tipoId;
        
    }
}