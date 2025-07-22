using Application.DTO.Edicao;
using Application.Interfaces;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Resolvers;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using WebApi.Consumers;
using Application.IPublishers;
using WebApi.Publishers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var environment = builder.Environment.EnvironmentName;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Services
builder.Services.AddTransient<IEdicaoService, EdicaoService>();
builder.Services.AddTransient<IEdicaoTemporaryService, EdicaoTemporaryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IMessagePublisher, MassTransitPublisher>();

// Repositories
builder.Services.AddTransient<IUserRepository, UserRepositoryEF>();
builder.Services.AddTransient<IEdicaoRepository, EdicaoRepositoryEF>();
builder.Services.AddTransient<IEdicaoTemporaryRepository, EdicaoTemporaryRepositoryEF>();

// Factories
builder.Services.AddTransient<IEdicaoFactory, EdicaoFactory>();
builder.Services.AddTransient<IUserFactory, UserFactory>();
builder.Services.AddTransient<IEdicaoTemporaryFactory, EdicaoTemporaryFactory>();

// Mappers
builder.Services.AddTransient<UserDataModelConverter>();
builder.Services.AddTransient<EdicaoDataModelConverter>();
builder.Services.AddTransient<EdicaoTemporaryDataModelConverter>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<DataModelMappingProfile>();
    cfg.CreateMap<Edicao, EdicaoDTO>();
});

var instanceInfo = InstanceInfo.InstanceId;
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TipoCreatedConsumer>();
    x.AddConsumer<EdicaoCreatedConsumer>();
    x.AddConsumer<UserCreatedConsumer>();

    x.AddSagaStateMachine<EdicaoStateMachine, EdicaoState>().InMemoryRepository();

    x.UsingRabbitMq((ctx, cfg) =>
   {
       cfg.Host("localhost", "/", h =>
       {
           h.Username("guest");
           h.Password("guest");
       });

       cfg.ReceiveEndpoint($"edicao-cmd-{instanceInfo}", e =>
       {
           e.ConfigureConsumer<EdicaoCreatedConsumer>(ctx);

           e.ConfigureConsumer<TipoCreatedConsumer>(ctx);
           e.ConfigureConsumer<UserCreatedConsumer>(ctx);
       });

       
       cfg.ReceiveEndpoint($"edicao-saga-queue-{instanceInfo}", e =>
       {
           e.ConfigureSaga<EdicaoState>(ctx);

       });
   });
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer(); // necessário para AddSwaggerGen funcionar
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
