
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

public class CreateEdicaoAsyncTests
{
    [Fact]
    public async Task Should_AddAndReturnEdicao()
    {
        var mockRepo = new Mock<IEdicaoRepository>();
        var mockFactory = new Mock<IEdicaoFactory>();
        var edicaoMock = new Mock<IEdicao>();

        mockRepo.Setup(r => r.AddAsync(edicaoMock.Object)).ReturnsAsync(edicaoMock.Object);

        var service = new EdicaoService(mockRepo.Object, mockFactory.Object);

        var result = await service.CreateEdicaoAsync(edicaoMock.Object);

        mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        Assert.Equal(edicaoMock.Object, result);
    }
}
