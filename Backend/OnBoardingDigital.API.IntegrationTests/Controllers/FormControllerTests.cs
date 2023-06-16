using Autofac;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnBoardingDigital.API.Application.Queries.Forms;
using OnBoardingDigital.API.Controllers;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
namespace OnBoardingDigital.API.IntegrationTests.Controllers;

public class FormControllerTests
{
    private readonly Mock<IFormRepository> _mockFormRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ISender> _mockSender;

    private readonly ExistFormQueryHandler _handlerUnderTest;
    private readonly FormController _controllerUnderTest;
    public FormControllerTests()
    {
        _mockFormRepository = new Mock<IFormRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockSender = new Mock<ISender>();
        _handlerUnderTest = new ExistFormQueryHandler(_mockFormRepository.Object);

        _controllerUnderTest = new FormController(_mockMapper.Object, _mockSender.Object);
    }

    [Fact]
    public async Task Exists_ValidId_FormExists_ReturnsOkResult()
    {
        // Arrange
        var formId = FormId.CreateUnique(); // Replace with a valid GUID string
        var query = new ExistFormQuery(formId.Value);
        var expectedResult = Error.NotFound(); // Form exists

        // Configure the mock dependencies
        _mockFormRepository.Setup(repo => repo.FormExistsAsync(It.Is<FormId>((id) => id.Equals(formId))))
            .ReturnsAsync(true);

        // Configure the mock dependencies
        _mockSender.Setup(repo => repo.Send(It.IsAny<ExistFormQuery>(), It.IsAny<CancellationToken>()))
            .Returns((ExistFormQuery request, CancellationToken token) => _handlerUnderTest.Handle(request, token));

        // Act
        var result = await _controllerUnderTest.Exists(formId.Value.ToString());

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task Exists_InvalidId_ReturnsBadRequestResult()
    {
        // Arrange
        var id = "invalid_id"; // Replace with an invalid GUID string

        // Act
        var result = await _controllerUnderTest.Exists(id);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("The field id is not a valid Guid.", badRequestResult.Value);
    }

    [Fact]
    public async Task Exists_ValidId_FormNotExists_ReturnsNotFoundResult()
    {
        // Arrange
        var formId = FormId.CreateUnique(); // Replace with a valid GUID string
        var query = new ExistFormQuery(formId.Value);
        var expectedResult = Error.NotFound(); // Form exists

        // Configure the mock dependencies
        _mockFormRepository.Setup(repo => repo.FormExistsAsync(It.Is<FormId>((id) => id.Equals(formId))))
            .ReturnsAsync(false);

        // Configure the mock dependencies
        _mockSender.Setup(repo => repo.Send(It.IsAny<ExistFormQuery>(), It.IsAny<CancellationToken>()))
            .Returns((ExistFormQuery request, CancellationToken token) => _handlerUnderTest.Handle(request, token));

        // Act
        var result = await _controllerUnderTest.Exists(formId.Value.ToString());

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}