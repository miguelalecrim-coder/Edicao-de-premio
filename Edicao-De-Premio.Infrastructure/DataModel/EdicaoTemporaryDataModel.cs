
using Domain.Interfaces;
using Domain.Models;

public class EdicaoTemporaryDataModel : IEdicaoTemporaryVisitor
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateOnly Date { get; set; }

    public string Descricao { get; set; }

    public EdicaoTemporaryDataModel(IEdicaoTemporary edicaoTemporary)
    {
        Id = edicaoTemporary.Id;
        UserId = edicaoTemporary.UserId;
        Date = edicaoTemporary.Date;
        Descricao = edicaoTemporary.Descricao;
    }


    public EdicaoTemporaryDataModel()
    {

    }

}