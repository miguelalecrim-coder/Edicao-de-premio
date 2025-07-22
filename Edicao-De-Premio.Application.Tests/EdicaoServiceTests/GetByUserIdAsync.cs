using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

public class GetByUserIdAsyncTests
{
    [Fact]
    public async Task Should_ReturnFilteredByUserId()
    {
        var userId = Guid.NewGuid();
        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();

        var edicoes = new List<IEdicao>
        {
            new Edicao(Guid.NewGuid(), userId, DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid()),
            new Edicao(Guid.NewGuid(), Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Now), Guid.NewGuid())
        };

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(edicoes);

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);
        var result = await service.GetByUserIdAsync(userId);

        Assert.Single(result);
    }
}
