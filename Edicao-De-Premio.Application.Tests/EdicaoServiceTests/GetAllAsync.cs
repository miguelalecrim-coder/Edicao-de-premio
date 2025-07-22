using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

public class GetAllAsyncTests
{
    [Fact]
    public async Task Should_ReturnAllDTOs()
    {
        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();

        var userId = Guid.NewGuid();
        var tipoId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Today);
        var edicaoId = Guid.NewGuid();

        var edicoes = new List<IEdicao>
        {
            new Edicao(edicaoId,userId,date,tipoId)
        };

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(edicoes);

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);
        var result = await service.GetAllAsync();

        Assert.Single(result);
    }
}
