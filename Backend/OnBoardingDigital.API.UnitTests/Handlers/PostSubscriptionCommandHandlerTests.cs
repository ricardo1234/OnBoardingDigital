using Moq;
using OnBoardingDigital.API.Application.Commands.Subscriptions;
using OnBoardingDigital.Contracts.Subscription;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Infrastructure.Repositories;
using OnBoardingDigital.Domain.FormAggregate.Entities;

namespace OnBoardingDigital.API.UnitTests.Handlers;

public class PostSubscriptionCommandHandlerTests
{
    private readonly Mock<ISubscriptionRepository> _mockSubscriptionRepository;
    private readonly Mock<IFormRepository> _mockFormRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly PostSubscriptionCommandHandler _handlerUnderTest;
    public PostSubscriptionCommandHandlerTests()
    {
        _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
        _mockFormRepository = new Mock<IFormRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handlerUnderTest = new PostSubscriptionCommandHandler(_mockSubscriptionRepository.Object, _mockFormRepository.Object, _mockUnitOfWork.Object);
    }


    [Fact]
    public async Task Handle_ValidRequest_CreatesSubscriptionAndCommitsUnitOfWork()
    {
        // Arrange
        var formId = FormId.CreateUnique();
        var sectionId = FormSectionId.CreateUnique();
        var fieldId_1 = FormFieldId.CreateUnique();
        var fieldId_2 = FormFieldId.CreateUnique();
        var email = "test@example.com";
        var command = new PostSubscriptionCommand(
            new SubscriptionRequest(
                formId.Value.ToString(),
                email,
                new List<SubscriptionAnswerRequest>
                {
                    new SubscriptionAnswerRequest(fieldId_1.Value.ToString(), 1, "answer1" ),
                    new SubscriptionAnswerRequest(fieldId_2.Value.ToString(), 1, "answer2" ),
                }
            ),
            new List<FileRequest>()
        );

        var form = Form.Create(
            formId,
            "Test",
            sectionId
            );
        var section = FormSection.Create(
                sectionId,
                "test",
                1,
                Repeatable.Create(),
                null);
        section.AddMultipleFormFields(
            new List<FormField>
            {
                FormField.Create(fieldId_1,1,true,"test",FieldType.Text,null,null,null,null,FieldTextSettings.Create(),null),
                FormField.Create(fieldId_2,1,true,"test2",FieldType.Text,null,null,null,null,FieldTextSettings.Create(),null)
            });

        form.AddFormSection(section);

        _mockFormRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<FormId>()))
            .ReturnsAsync(form);

        _mockSubscriptionRepository.Setup(repo => repo.AddAsync(It.IsAny<Subscription>()))
            .ReturnsAsync(Subscription.CreateNew(email,formId)); // Replace with the created subscription instance

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        _mockFormRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<FormId>()), Times.Once);
        _mockSubscriptionRepository.Verify(repo => repo.AddAsync(It.IsAny<Subscription>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task Handle_FormNotFound_ReturnsNotFoundError()
    {
        // Arrange
        var formId = FormId.CreateUnique();
        var sectionId = FormSectionId.CreateUnique();
        var fieldId_1 = FormFieldId.CreateUnique();
        var fieldId_2 = FormFieldId.CreateUnique();
        var email = "test@example.com"; // Replace with a valid email address
        var command = new PostSubscriptionCommand(
            new SubscriptionRequest(
                formId.Value.ToString(),
                email,
                new List<SubscriptionAnswerRequest>
                {
                    new SubscriptionAnswerRequest(fieldId_1.Value.ToString(), 1, "answer1" ),
                    new SubscriptionAnswerRequest(fieldId_2.Value.ToString(), 1, "answer2" ),
                }
            ),
            new List<FileRequest>()
        );

        _mockFormRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<FormId>()))
            .ReturnsAsync((Form)null);

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Form.NotFound", result.FirstError.Code);
        Assert.Equal("Form was not found.", result.FirstError.Description);
    }

    [Fact]
    public async Task Handle_InvalidEmail_ReturnsValidationError()
    {
        // Arrange
        var formId = FormId.CreateUnique();
        var sectionId = FormSectionId.CreateUnique();
        var fieldId_1 = FormFieldId.CreateUnique();
        var fieldId_2 = FormFieldId.CreateUnique();
        var invalidEmail = ""; // Replace with an invalid email address
        var command = new PostSubscriptionCommand(
            new SubscriptionRequest(
                formId.Value.ToString(),
                invalidEmail,
                new List<SubscriptionAnswerRequest>
                {
                    new SubscriptionAnswerRequest(fieldId_1.Value.ToString(), 1, "answer1" ),
                    new SubscriptionAnswerRequest(fieldId_2.Value.ToString(), 1, "answer2" ),
                }
            ),
            new List<FileRequest>()
        );

        _mockFormRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<FormId>()))
           .ReturnsAsync(Form.CreateNew("teste", sectionId));

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Subscription.RequiredField", result.FirstError.Code);
        Assert.Equal("Email is required", result.FirstError.Description);
    }

    [Fact]
    public async Task Handle_RequiredFieldMissing_ReturnsValidationError()
    {
        // Arrange
        var formId = FormId.CreateUnique();
        var sectionId = FormSectionId.CreateUnique();
        var fieldId_1 = FormFieldId.CreateUnique();
        var fieldId_2 = FormFieldId.CreateUnique();
        var email = "test@example.com"; // Replace with a valid email address
        var command = new PostSubscriptionCommand(
            new SubscriptionRequest(
                formId.Value.ToString(),
                email,
                new List<SubscriptionAnswerRequest>
                {
                    new SubscriptionAnswerRequest(fieldId_1.Value.ToString(), 1, "answer1" ),
                }
            ),
            new List<FileRequest>()
        );

        var form = Form.Create(
            formId,
            "Test",
            sectionId
            );
        var section = FormSection.Create(
                sectionId,
                "test",
                1,
                Repeatable.Create(),
                null);
        section.AddMultipleFormFields(
            new List<FormField>
            {
                FormField.Create(fieldId_1,1,true,"test",FieldType.Text,null,null,null,null,FieldTextSettings.Create(),null),
                FormField.Create(fieldId_2,1,true,"test2",FieldType.Text,null,null,null,null,FieldTextSettings.Create(),null)
            });

        form.AddFormSection(section);

        _mockFormRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<FormId>()))
            .ReturnsAsync(form);

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Subscription.RequiredField", result.FirstError.Code);
        Assert.Equal("The field with name test2 is required.", result.FirstError.Description);
    }

    [Fact]
    public async Task Handle_InvalidFieldAnswer_ReturnsValidationError()
    {
        // Arrange

        var formId = FormId.CreateUnique();
        var sectionId = FormSectionId.CreateUnique();
        var fieldId_1 = FormFieldId.CreateUnique();
        var fieldId_2 = FormFieldId.CreateUnique();
        var email = "test@example.com"; // Replace with a valid email address
        var command = new PostSubscriptionCommand(
            new SubscriptionRequest(
                formId.Value.ToString(),
                email,
                new List<SubscriptionAnswerRequest>
                {
                    new SubscriptionAnswerRequest(fieldId_1.Value.ToString(), 1, "answer1" ),
                    new SubscriptionAnswerRequest(fieldId_2.Value.ToString(), 1, "answer2" ),
                }
            ),
            new List<FileRequest>()
        );

        var form = Form.Create(
            formId,
            "Test",
            sectionId
            );
        var section = FormSection.Create(
                sectionId,
                "test",
                1,
                Repeatable.Create(),
                null);
        section.AddMultipleFormFields(
            new List<FormField>
            {
                FormField.Create(fieldId_1,1,true,"test",FieldType.Text,null,null,null,null,FieldTextSettings.Create(),null),
                FormField.Create(fieldId_2,1,true,"test2",FieldType.Text,null,null,null,null,FieldTextSettings.Create(1),null)
            });

        form.AddFormSection(section);

        _mockFormRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<FormId>()))
            .ReturnsAsync(form);

        // Act
        var result = await _handlerUnderTest.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Subscription.InvalidField", result.FirstError.Code);
        Assert.Equal("The field with name test2 has too many characters.", result.FirstError.Description);
    }
}
