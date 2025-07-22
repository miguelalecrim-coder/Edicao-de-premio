

using Application.DTO;
using Application.Interfaces;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;

namespace Application.Services;


public class EdicaoService : IEdicaoService
{
    private IEdicaoRepository _edicaoRepository;
    private IEdicaoFactory _edicaoFactory;


    public EdicaoService(IEdicaoRepository edicaoRepository, IEdicaoFactory edicaoFactory)
    {
        _edicaoRepository = edicaoRepository;
        _edicaoFactory = edicaoFactory;
    }

    public async Task<IEdicao?> AddEdicaoReferenceAsync(Guid edicaoId, Guid userId, DateOnly date, Guid tipoId)
    {
        var edicaoAlreadyExits = await _edicaoRepository.AlreadyExistsAsync(edicaoId);

        if (edicaoAlreadyExits) return null;

        var visitor = new EdicaoDataModel()
        {
            Id = edicaoId,
            UserId = userId,
            Date = date,
            TipoId = tipoId
        };

        var newEdicao = _edicaoFactory.Create(visitor);

        return await _edicaoRepository.AddAsync(newEdicao);
    }

    public async Task<Result<CreatedEdicaoDTO>> Create(CreateEdicaoDTO edicaoDTO)
    {
        IEdicao newEdicao;

        try
        {
            newEdicao = await _edicaoFactory.Create(edicaoDTO.UserId, edicaoDTO.Date, edicaoDTO.TipoId);
            newEdicao = await _edicaoRepository.AddAsync(newEdicao);

            var result = new CreatedEdicaoDTO(newEdicao.Id, newEdicao.UserId, newEdicao.Date, newEdicao.TipoId);

            return Result<CreatedEdicaoDTO>.Success(result);
        }
        catch (ArgumentException ex)
        {
            return Result<CreatedEdicaoDTO>.Failure(Error.InternalServerError(ex.Message));
        }
    }

    public async Task<IEdicao> CreateEdicaoAsync(IEdicao edicao)
    {
        var edic = await _edicaoRepository.AddAsync(edicao);
        await _edicaoRepository.SaveChangesAsync();

        return edic;

    }

    public async Task<IEnumerable<CreatedEdicaoDTO>> GetAllAsync()
    {
        var edicoes = await _edicaoRepository.GetAllAsync();

        return edicoes.Select(e => new CreatedEdicaoDTO(e.Id, e.UserId, e.Date, e.TipoId));

    }

    public async Task<IEnumerable<CreatedEdicaoDTO>> GetByUserIdAsync(Guid userId)
    {
        var edicoes = await _edicaoRepository.GetAllAsync();
    var filtered = edicoes.Where(e => e.UserId == userId);
    return filtered.Select(e => new CreatedEdicaoDTO(e.Id, e.UserId, e.Date, e.TipoId));
    }

    public async Task<IEnumerable<CreatedEdicaoDTO>> SearchAsync(Guid? userId, DateOnly? date, Guid? tipoId)
    {
        var edicoes = await _edicaoRepository.GetAllAsync();

    if (userId.HasValue)
        edicoes = edicoes.Where(e => e.UserId == userId.Value);

    if (date.HasValue)
        edicoes = edicoes.Where(e => e.Date == date.Value);

    if (tipoId.HasValue)
        edicoes = edicoes.Where(e => e.TipoId == tipoId.Value);

    return edicoes.Select(e => new CreatedEdicaoDTO(e.Id, e.UserId, e.Date, e.TipoId));
    }
}
