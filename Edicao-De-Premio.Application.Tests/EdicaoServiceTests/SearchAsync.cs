using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

public class SearchAsyncTests
{
    [Fact]
    public async Task Should_FilterByAllParams()
    {
        var userId = Guid.NewGuid();
        var date = DateOnly.FromDateTime(DateTime.Today);
        var tipoId = Guid.NewGuid();

        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();

        var edicoes = new List<IEdicao>
        {
            new Edicao(Guid.NewGuid(), userId, date, tipoId),
            new Edicao(Guid.NewGuid(), Guid.NewGuid(), date, tipoId)
        };

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(edicoes);

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);
        var result = await service.SearchAsync(userId, date, tipoId);

        Assert.Single(result);
    }
}
