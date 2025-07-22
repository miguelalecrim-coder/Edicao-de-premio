using Domain.Models;

namespace Domain.Messages;


public record EdicaoCreatedMessage(Guid Id, Guid UserId, DateOnly Date, Guid TipoId);