using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class EdicaoDataModelConverter : ITypeConverter<EdicaoDataModel, Edicao>
{
    private readonly IEdicaoFactory _factory;

    public EdicaoDataModelConverter(IEdicaoFactory factory)
    {
        _factory = factory;
    }

    public Edicao Convert(EdicaoDataModel source, Edicao destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}