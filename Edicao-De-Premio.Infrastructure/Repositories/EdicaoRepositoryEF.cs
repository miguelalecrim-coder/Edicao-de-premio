using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EdicaoRepositoryEF : GenericRepositoryEF<IEdicao, Edicao, EdicaoDataModel>, IEdicaoRepository
{
    private readonly IMapper _mapper;

    public EdicaoRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<bool> AlreadyExistsAsync(Guid edicaoId)
    {
        return await _context.Set<EdicaoDataModel>().AnyAsync(c => c.Id == edicaoId);
    }

    public async Task<bool> ExistsByUserIdAsync(Guid userId)
    {
        return await _context.Set<EdicaoDataModel>()
                        .AnyAsync(c => c.UserId == userId);
    }

    public override IEdicao? GetById(Guid id)
    {
        var edicaoDM = this._context.Set<EdicaoDataModel>()
                        .FirstOrDefault(c => c.Id == id);

        if (edicaoDM == null)
            return null;

        var edicao = _mapper.Map<EdicaoDataModel, Edicao>(edicaoDM);
        return edicao;
    }

    public override async Task<IEdicao?> GetByIdAsync(Guid id)
    {
        var edicaoDM = await this._context.Set<EdicaoDataModel>()
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (edicaoDM == null)
            return null;

        var edicao = _mapper.Map<EdicaoDataModel, Edicao>(edicaoDM);

        return edicao;
    }

    public async Task<IEnumerable<IEdicao>> GetByUserIdAsync(Guid userId)
    {
        var edicoes = await _context.Set<EdicaoDataModel>()
                        .Where(e => e.UserId == userId).ToListAsync();

        return _mapper.Map<IEnumerable<Edicao>>(edicoes);
    }

    public async Task<bool> IsRepeated(IEdicao edicao)
    {
        return await this._context.Set<EdicaoDataModel>()
            .AnyAsync(c => c.UserId == edicao.UserId);
    }

    public async Task<IEnumerable<IEdicao>> SearchAsync(Guid? userId, DateOnly? data, Guid? tipoDePremioId)
    {
        var query = _context.Set<EdicaoDataModel>().AsQueryable();

        if (userId.HasValue)
            query = query.Where(e => e.UserId == userId.Value);

        if (data.HasValue)
            query = query.Where(e => e.Date == data.Value);

        if (tipoDePremioId.HasValue)
            query = query.Where(e => e.TipoId == tipoDePremioId.Value);

        var edicoes = await query.ToListAsync();

        return _mapper.Map<IEnumerable<Edicao>>(edicoes);
    }
}