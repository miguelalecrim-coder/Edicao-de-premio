using Domain.Models;

namespace Domain.Messages;

public record TipoCreatedMessage(Guid Id, string Descricao);