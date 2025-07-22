using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;

public class AddEdicaoReferenceTests
{

    [Fact]
    public async Task Should_ReturnNull_When_EdicaoAlreadyExists()
    {
        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();

        mockRepo.Setup(r => r.AlreadyExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);

        var result = await service.AddEdicaoReferenceAsync(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
public async Task Should_AddEdicao_When_NotExists()
{
    // Arrange
    var edicaoId = Guid.NewGuid();
    var userId = Guid.NewGuid();
    var tipoId = Guid.NewGuid();
    var date = DateOnly.FromDateTime(DateTime.Today);

    var mockRepo = new Mock<IEdicaoRepository>();
    var mockFactory = new Mock<IEdicaoFactory>();

    var edicao = new Edicao(edicaoId, userId, date, tipoId);

    mockRepo
        .Setup(r => r.AlreadyExistsAsync(edicaoId))
        .ReturnsAsync(false);

    mockFactory
        .Setup(f => f.Create(It.IsAny<EdicaoDataModel>()))
        .Returns(edicao);

    mockRepo
        .Setup(r => r.AddAsync(edicao))
        .ReturnsAsync(edicao);

    var service = new EdicaoService(mockRepo.Object, mockFactory.Object);

    // Act
    var result = await service.AddEdicaoReferenceAsync(edicaoId, userId, date, tipoId);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(edicao.Id, result!.Id);
    Assert.Equal(edicao.UserId, result.UserId);
    Assert.Equal(edicao.TipoId, result.TipoId);
    Assert.Equal(edicao.Date, result.Date);
}

}