


using Application.DTO;
using Domain.Interfaces;

namespace Application.Interfaces;

public interface IEdicaoService
{
    Task<IEdicao?> AddEdicaoReferenceAsync(Guid edicaoId, Guid userId, DateOnly date, Guid tipoId);
    Task<Result<CreatedEdicaoDTO>> Create(CreateEdicaoDTO edicaoDTO);
    Task<IEdicao> CreateEdicaoAsync(IEdicao edicao);
    Task<IEnumerable<CreatedEdicaoDTO>> GetAllAsync();
    Task<IEnumerable<CreatedEdicaoDTO>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<CreatedEdicaoDTO>> SearchAsync(Guid? userId, DateOnly? date, Guid? tipoId);

}