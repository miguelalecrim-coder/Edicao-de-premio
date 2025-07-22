namespace Domain.Models;


public class CreatedTemporaryDTO
{
    public string UserId { get; set; } = default!;

    public string EdicaoId { get; set; } = default!;

    public DateOnly Date { get; set; } = default!;

    public string Descricao { get; set; } = default!;
}