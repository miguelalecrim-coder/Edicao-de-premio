
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;


public class TipoFactory : ITipoFactory
{

    private readonly ITipoRepository _tipoRepository;

    public TipoFactory(ITipoRepository tipoRepository)
    {
        _tipoRepository = tipoRepository;
    }

    

    public Tipo Create(ITipoVisitor tipoVisitor)
    {
        return new Tipo(tipoVisitor.Id);
    }

    public async Task<Tipo> Create(Guid id)
    {
        var alreadyExists = await _tipoRepository.Exists(id);

        if (alreadyExists)
        {
            return null;
        }

        return new Tipo(id);

    }
}