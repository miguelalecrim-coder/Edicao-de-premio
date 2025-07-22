namespace Application.DTO;


public class CreatedEdicaoDTO
{
    public Guid UserId { get; set; }

    public Guid EdicaoId { get; set; }

    public DateOnly Date { get; set; }

    public Guid TipoId { get; set; }


    public CreatedEdicaoDTO(Guid userId, Guid edicaoId, DateOnly date, Guid tipoId)
    {
        UserId = userId;
        EdicaoId = edicaoId;
        Date = date;
        TipoId = tipoId;
    }
}