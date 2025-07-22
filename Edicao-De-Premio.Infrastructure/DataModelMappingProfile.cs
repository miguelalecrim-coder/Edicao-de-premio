


using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;


public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {

        CreateMap<User, UserDataModel>();
        CreateMap<Edicao, EdicaoDataModel>();
        CreateMap<EdicaoDataModel, Edicao>().ConvertUsing<EdicaoDataModelConverter>();
        CreateMap<UserDataModel, User>()
            .ConvertUsing<UserDataModelConverter>();
        CreateMap<EdicaoTemporaryDataModel, EdicaoTemporary>()
            .ConvertUsing<EdicaoTemporaryDataModelConverter>();
    }
}