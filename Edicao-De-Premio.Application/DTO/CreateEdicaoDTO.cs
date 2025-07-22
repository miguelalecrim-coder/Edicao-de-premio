namespace Application.DTO;


public record CreateEdicaoDTO
{
    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }

    public Guid TipoId { get; set; }


    public CreateEdicaoDTO(Guid userId, DateOnly date, Guid tipoId)
    {
        UserId = userId;
        Date = date;
        TipoId = tipoId;
    }
}