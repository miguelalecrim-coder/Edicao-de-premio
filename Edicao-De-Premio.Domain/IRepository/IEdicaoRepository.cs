using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;


public interface IEdicaoRepository : IGenericRepositoryEF<IEdicao, Edicao, IEdicaoVisitor>
{
    Task<bool> ExistsByUserIdAsync(Guid userId);
    Task<bool> AlreadyExistsAsync(Guid edicaoId);

    Task<bool> IsRepeated(IEdicao edicao);
    Task<IEnumerable<IEdicao>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<IEdicao>> SearchAsync(Guid? userId, DateOnly? data, Guid? tipoDePremioId);
    
}