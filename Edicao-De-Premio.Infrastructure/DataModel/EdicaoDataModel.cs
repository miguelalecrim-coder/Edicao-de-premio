

using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel;


public class EdicaoDataModel : IEdicaoVisitor
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }

    public Guid TipoId { get; set; }


    public EdicaoDataModel(IEdicao edicao)
    {
        Id = edicao.Id;
        UserId = edicao.UserId;
        Date = edicao.Date;
        TipoId = edicao.TipoId;
    }

    public EdicaoDataModel()
    {}
}