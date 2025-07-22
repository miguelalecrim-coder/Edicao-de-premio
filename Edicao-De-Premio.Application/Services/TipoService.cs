using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Microsoft.VisualBasic;

namespace Application.Services;


public class TipoService
{
    private readonly ITipoRepository _tipoRepository;

    private readonly ITipoFactory _tipoFactory;

    public TipoService(ITipoRepository tipoRepository, ITipoFactory tipoFactory)
    {
        _tipoRepository = tipoRepository;
        _tipoFactory = tipoFactory;
    }

    public async Task<ITipo?> AddTipoReferenceAsync(Guid tipoId)
    {
        var newTipo = await _tipoFactory.Create(tipoId);

        if (newTipo == null) return null;

        return await _tipoRepository.AddAsync(newTipo);
    }

}