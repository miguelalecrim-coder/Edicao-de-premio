
namespace Application.Messages.Edicao;


public record CreateEdicaoRequestMessage(Guid UserId, DateOnly Date, string Descricao);