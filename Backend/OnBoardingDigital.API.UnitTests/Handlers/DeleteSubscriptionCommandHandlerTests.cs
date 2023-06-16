using Moq;
using OnBoardingDigital.API.Application.Commands.Subscriptions;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.API.UnitTests.Handlers;

public class DeleteSubscriptionCommandHandlerTests
{
    private readonly Mock<ISubscriptionRepository> _mockSubscriptionRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly DeleteSubscriptionCommandHandler _handlerUnderTest;
    public DeleteSubscriptionCommandHandlerTests()
    {
        _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handlerUnderTest = new DeleteSubscriptionCommandHandler(_mockSubscriptionRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ValidId_DeletesSubscriptionAndCommitsUnitOfWork()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid().ToString(); // Replace with your desired subscription ID
        var command = new DeleteSubscriptionCommand(subscriptionId);
        var existingSubscription = Subscription.CreateNew("teste", FormId.CreateUnique()); // Replace with an existing subscription instance

        _mockSubscriptionRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<SubscriptionId>()))
            .ReturnsAsync(existingSubscription);

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        _mockSubscriptionRepository.Verify(repo => repo.Remove(existingSubscription), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        Assert.False(result.IsError);
        Assert.Equal(existingSubscription, result.Value);
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsValidationError()
    {
        // Arrange
        var invalidId = "invalid_id";
        var command = new DeleteSubscriptionCommand(invalidId);

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Subscription.InvalidId", result.FirstError.Code);
        Assert.Equal("The id provided is not a GUID.", result.FirstError.Description);
    }

    [Fact]
    public async Task Handle_SubscriptionNotFound_ReturnsNotFoundError()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid().ToString(); // Replace with a subscription ID that does not exist
        var command = new DeleteSubscriptionCommand(subscriptionId);

        _mockSubscriptionRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<SubscriptionId>()))
            .ReturnsAsync((Subscription)null);

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Subscription.NotFound", result.FirstError.Code);
        Assert.Equal("Subscription was not found.", result.FirstError.Description);
    }
}