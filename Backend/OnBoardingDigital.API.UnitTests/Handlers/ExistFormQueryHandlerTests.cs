using Moq;
using OnBoardingDigital.API.Application.Queries.Forms;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;

namespace OnBoardingDigital.API.UnitTests.Handlers;

public class ExistFormQueryHandlerTests
{
    private readonly Mock<IFormRepository> _mockFormRepository;

    private readonly ExistFormQueryHandler _handlerUnderTest;
    public ExistFormQueryHandlerTests()
    {
        _mockFormRepository = new Mock<IFormRepository>();

        _handlerUnderTest = new ExistFormQueryHandler(_mockFormRepository.Object);
    }

    [Fact]
    public async Task Handle_FormExists_ReturnsForm()
    {
        // Arrange
        var formId = FormId.CreateUnique(); // Replace with your desired form ID
        var query = new ExistFormQuery(formId.Value);
        var existingForm = true;

        _mockFormRepository.Setup(repo => repo.FormExistsAsync(It.Is<FormId>((f) => f.Equals(formId))))
            .ReturnsAsync(existingForm);

        // Act
        var result = await _handlerUnderTest.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task Handle_FormDoesNotExist_ReturnsNotFoundError()
    {
        // Arrange
        var formId = FormId.CreateUnique(); // Replace with your desired form ID
        var query = new ExistFormQuery(formId.Value);
        var existingForm = false;

        _mockFormRepository.Setup(repo => repo.FormExistsAsync(It.Is<FormId>((f) => !f.Equals(formId))))
            .ReturnsAsync(existingForm);

        // Act
        var result = await _handlerUnderTest.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Form.NotFound", result.FirstError.Code);
        Assert.Equal("Form was not found.", result.FirstError.Description);
    }
}

