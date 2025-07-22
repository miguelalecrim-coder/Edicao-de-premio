namespace Domain.Models;


public class CreateTemporaryDTO
{
    public Guid UserId { get; set; } = default!;

    public DateOnly Date { get; set; } = default!;

    public string Descricao { get; set; } = default!;


      public CreateTemporaryDTO(Guid userId, DateOnly date, string descricao)
    {
        UserId = userId;
        Date = date;
        Descricao = descricao;
    }
}