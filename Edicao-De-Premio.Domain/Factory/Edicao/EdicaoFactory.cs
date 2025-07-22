using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;


public class EdicaoFactory : IEdicaoFactory
{
    private readonly IEdicaoRepository _edicaoRepository;
    private readonly IUserRepository _userRepository;

    public EdicaoFactory(IEdicaoRepository edicaoRepository, IUserRepository userRepository)
    {
        _edicaoRepository = edicaoRepository;
        _userRepository = userRepository;
    }

    public Edicao ConvertFromTemporary(IEdicaoTemporary edicaoTemporary, Guid tipoId)
    {
        return new Edicao(edicaoTemporary.UserId, edicaoTemporary.Date, tipoId);
    }

    public async Task<Edicao> Create(Guid userId, DateOnly date, Guid tipoId)
    {
        return new Edicao(userId, date, tipoId);
    }

    public Edicao Create(Guid edicaoId, Guid userId, DateOnly date, Guid tipoId)
    {
        return new Edicao(edicaoId, userId, date, tipoId);
    }

    public Edicao Create(IEdicaoVisitor edicaoVisitor)
    {
        return new Edicao(edicaoVisitor.Id, edicaoVisitor.UserId, edicaoVisitor.Date, edicaoVisitor.TipoId);
    }
}