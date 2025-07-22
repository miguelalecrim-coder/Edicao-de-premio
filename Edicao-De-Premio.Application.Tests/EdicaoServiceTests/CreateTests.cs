using Application.DTO;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

public class CreateTests
{

    [Fact]
    public async Task Should_ReturnSuccess_When_ValidDTO()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var tipoId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Today);
        var edicaoId = Guid.NewGuid();

        var dto = new CreateEdicaoDTO(userId, date, tipoId);

        var edicao = new Edicao(edicaoId, userId, date, tipoId);

        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();

        mockFactory.Setup(f => f.Create(userId, date, tipoId)).ReturnsAsync(edicao);
        mockRepo.Setup(r => r.AddAsync(It.IsAny<IEdicao>())).ReturnsAsync(edicao);

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);

        // Act
        var result = await service.Create(dto);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
      [Fact]
    public async Task Should_ReturnFailure_When_ExceptionThrown()
    {
        var userId = Guid.NewGuid();
        var tipoId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Today);
        var edicaoId = Guid.NewGuid();

        var dto = new CreateEdicaoDTO(userId, date, tipoId);
        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();

        mockFactory.Setup(f => f.Create(dto.UserId, dto.Date, dto.TipoId))
                   .ThrowsAsync(new ArgumentException("Invalid data"));

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);

        var result = await service.Create(dto);

        Assert.False(result.IsSuccess);
        
    }


}