using Domain.Models;

public class CreatedEdicaoTipoDTO
{
    public Guid UserId { get; set; } = default!;

    public DateOnly Date { get; set; } = default!;

    public string Descricao { get; set; } = default!;

}