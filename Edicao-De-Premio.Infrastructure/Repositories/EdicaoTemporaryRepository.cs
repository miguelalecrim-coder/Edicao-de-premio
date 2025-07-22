using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class EdicaoTemporaryRepositoryEF : GenericRepositoryEF<IEdicaoTemporary, EdicaoTemporary, EdicaoTemporaryDataModel>, IEdicaoTemporaryRepository
{

    private readonly IMapper _mapper;

    public EdicaoTemporaryRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        var trackedEntity = _context.ChangeTracker.Entries<EdicaoTemporaryDataModel>()
        .FirstOrDefault(e => e.Entity.Id == id);

        if (trackedEntity != null)
            trackedEntity.State = EntityState.Detached;

        var entity = await _context.Set<EdicaoTemporaryDataModel>().FindAsync(id);
        if (entity == null)
            return;

        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<IEdicaoTemporary?> GetByDescricaoAsync(string descricao)
    {
        var dataModel = await _context.Set<EdicaoTemporaryDataModel>().FirstOrDefaultAsync(c => c.Descricao == descricao);

        if (dataModel == null)
            return null;

        var domainModel = _mapper.Map<EdicaoTemporaryDataModel, EdicaoTemporary>(dataModel);
        return domainModel;
    }

    public override IEdicaoTemporary? GetById(Guid id)
    {
        var dataModel = _context.Set<EdicaoTemporaryDataModel>().FirstOrDefault(c => c.Id == id);
        if (dataModel == null) return null;

        var domainModel = _mapper.Map<EdicaoTemporaryDataModel, EdicaoTemporary>(dataModel);
        return domainModel;
    }

    public override async Task<IEdicaoTemporary?> GetByIdAsync(Guid id)
    {
        var dataModel = await _context.Set<EdicaoTemporaryDataModel>().FirstOrDefaultAsync(c => c.Id == id);
        if (dataModel == null) return null;

        var domainModel = _mapper.Map<EdicaoTemporaryDataModel, EdicaoTemporary>(dataModel);
        return domainModel;
    }
}
