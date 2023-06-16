using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnBoardingDigital.API.Application.Commands.Subscriptions;
using OnBoardingDigital.API.Application.Queries.Forms;
using OnBoardingDigital.API.Application.Queries.Subscriptions;
using OnBoardingDigital.API.Controllers;
using OnBoardingDigital.Contracts.Subscription;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.API.IntegrationTests.Controllers;

public class SubscriptionControllerTests
{
    private readonly Mock<IFormRepository> _mockFormRepository;
    private readonly Mock<ISubscriptionRepository> _mockSubscriptionRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ISender> _mockSender;

    private readonly GetAllSubscriptionQueryHandler _handlerUnderTest_1;
    private readonly PostSubscriptionCommandHandler _handlerUnderTest_2;
    private readonly SubscriptionController _controllerUnderTest;
    public SubscriptionControllerTests()
    {
        _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
        _mockFormRepository = new Mock<IFormRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockSender = new Mock<ISender>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handlerUnderTest_1 = new GetAllSubscriptionQueryHandler(_mockSubscriptionRepository.Object);
        _handlerUnderTest_2 = new PostSubscriptionCommandHandler(_mockSubscriptionRepository.Object, _mockFormRepository.Object, _mockUnitOfWork.Object);

        _controllerUnderTest = new SubscriptionController(_mockSender.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task SubscriptionController_GetByEmail_ValidEmail_ReturnsOkResultWithSubscriptions()
    {
        // Arrange
        var email = "test@example.com";
        var subscriptions = new List<Subscription>
        {
            Subscription.CreateNew(email, FormId.CreateUnique()),
            Subscription.CreateNew(email, FormId.CreateUnique()),
            Subscription.CreateNew(email, FormId.CreateUnique()),
        };

        _mockSubscriptionRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(subscriptions);

        _mockSender.Setup(repo => repo.Send(It.IsAny<GetAllSubscriptionQuery>(), It.IsAny<CancellationToken>()))
            .Returns((GetAllSubscriptionQuery request, CancellationToken token) => _handlerUnderTest_1.Handle(request, token));

        _mockMapper.Setup(map => map.Map<List<AllSubscriptionResponse>>(It.IsAny<List<Subscription>>()))
            .Returns(
            new List<AllSubscriptionResponse>() { 
                new AllSubscriptionResponse("", "", "", DateTime.Now),
                new AllSubscriptionResponse("", "", "", DateTime.Now),
                new AllSubscriptionResponse("", "", "", DateTime.Now) 
            });

        // Act
        var result = await _controllerUnderTest.GetByEmail(email);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        var response = Assert.IsType<List<AllSubscriptionResponse>>(okResult.Value);
        Assert.Equal(subscriptions.Count, response.Count);
    }
}

