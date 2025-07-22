using AutoMapper;
using Domain.Factory;
using Domain.Models;

namespace Infrastructure.Resolvers;


public class EdicaoTemporaryDataModelConverter : ITypeConverter<EdicaoTemporaryDataModel, EdicaoTemporary>
{


    private readonly IEdicaoTemporaryFactory _factory;

    public EdicaoTemporaryDataModelConverter(IEdicaoTemporaryFactory factory)
    {
        _factory = factory;
    }
    
    public EdicaoTemporary Convert(EdicaoTemporaryDataModel source, EdicaoTemporary destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}