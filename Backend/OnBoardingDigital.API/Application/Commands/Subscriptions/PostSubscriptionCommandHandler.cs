using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Contracts.Subscription;
using System.Text.RegularExpressions;
using OnBoardingDigital.Domain.SubscriptionAggregate.Entities;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public class PostSubscriptionCommandHandler : IRequestHandler<PostSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository subscriptionRepository;
    private readonly IFormRepository formRepository;
    private readonly IUnitOfWork unitOfWork;

    public PostSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IFormRepository formRepository, IUnitOfWork unitOfWork)
    {
        this.subscriptionRepository = subscriptionRepository;
        this.formRepository = formRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(PostSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var formId = FormId.CreateFromString(request.Subscription.FormId);

        var form = await formRepository.GetByIdAsync(formId);

        if(form is null)
            return Error.NotFound("Form.NotFound", "Form was not found.");

        if (string.IsNullOrEmpty(request.Subscription.Email))
            return Error.Validation("Subscription.RequiredField","Email is required");

        Subscription subscritpion = Subscription.CreateNew(request.Subscription.Email, formId);

        var validation = SectionsRecursive(form.FirstSection, form, request, subscritpion);

        if (validation.IsError)
            return validation.FirstError;

        var result = await subscriptionRepository.AddAsync(validation.Value);
        
        await unitOfWork.CommitAsync();

        return result;
    }
    private ErrorOr<Subscription> SectionsRecursive(FormSectionId sectionId, Form form, PostSubscriptionCommand request, Subscription subscritpion)
    {
        var section = form.Sections.FirstOrDefault(section => section.Id == sectionId);

        if (section is null)
            return Error.Failure("Form.BadConfiguration","Form have a bad configuration.");

        var defaultSection = section.DefaultNextSection;

        foreach (var field in section.Fields.OrderBy(f => f.Order))
        {
            if (field.Type.Equals(FieldType.Information))
                continue;

            var answer = request.Subscription.Answers.Where(a => a.FieldId?.ToLower() == field.Id.Value.ToString()?.ToLower()).ToList();

            if (field.Required && answer.Count == 0)
                return Error.Validation("Subscription.RequiredField", $"The field with name {field.Description} is required.");
           
            if (answer.Count == 0)
                continue;

            if (!section.Repeatable.CanRepeat && answer.Count != 1)
                return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} cannot be repeated.");

            if (section.Repeatable.CanRepeat && section.Repeatable.NumberOfReapeats is not null && answer.Count > section.Repeatable.NumberOfReapeats)
                return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} has too many answers.");

            for (int i = 0; i < answer.Count; i++)
            {
                Answer? ans = null;

                var result = field.Type.Id switch
                {
                    (int)AttributeFieldTypes.File => ValidateFieldFile(field, request.Files),
                    (int)AttributeFieldTypes.Text => ValidateFieldText(field, answer[i]),
                    (int)AttributeFieldTypes.Choice => ValidateFieldChoice(field, answer[i]),
                    (int)AttributeFieldTypes.Number => ValidateFieldNumber(field, answer[i]),
                    (int)AttributeFieldTypes.Options => ValidateFieldOption(field, answer[i]),

                    _ => Error.Failure("Form.BadConfiguration", "The type of field does not exist."),
                };

                if (result.IsError)
                    return result.FirstError;

                ans = result.Value;

                subscritpion.AddAnswer(SubscriptionAnswer.CreateNew(field.Id, i+1, ans));

                if(i+1 == answer.Count)
                {
                    FormSectionId? nextQuestion = null;
                    if (field.Type.Id == (int)AttributeFieldTypes.Options)
                        nextQuestion = field.OptionsSettings?.Options.FirstOrDefault(opt => opt.Value.Equals(answer[i].Answer))?.NextSection;
                    
                    if (field.Type.Id == (int)AttributeFieldTypes.Choice)
                        nextQuestion = field.ChoiceSettings?.NextSection;

                    if (nextQuestion is not null)
                        defaultSection = nextQuestion;
                }
            }
        }

        if (defaultSection is null)
            return subscritpion;

        return SectionsRecursive(defaultSection, form, request, subscritpion);
    }

    private ErrorOr<Answer> ValidateFieldText(FormField field, SubscriptionAnswerRequest answer)
    {
        if (field.TextSettings?.ValidationExpression is not null)
        {
            var regex = new Regex(field.TextSettings.ValidationExpression);
            if (!regex.IsMatch(answer.Answer))
                return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is not valid.");
        }

        if (field.TextSettings?.CharMaximum is not null)
        {
            if (answer.Answer.Length > field.TextSettings?.CharMaximum)
                return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} has too many characters.");
        }

        if (field.TextSettings?.CharMinimum is not null)
        {
            if (answer.Answer.Length < field.TextSettings?.CharMinimum)
                return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is short on characters.");
        }

        return Answer.CreateText(answer.Answer);
    }
    private ErrorOr<Answer> ValidateFieldChoice(FormField field, SubscriptionAnswerRequest answer)
    {
        string[] validTrueChoices = { "true", "1", "verdadeiro" };
        string[] validFalseChoices = { "false", "0", "falso" };
        if (validFalseChoices.Contains(answer.Answer?.ToLower()))
            return Answer.CreateChoice(false.ToString());
        else if (validTrueChoices.Contains(answer.Answer?.ToLower()))
            return Answer.CreateChoice(true.ToString());
        else
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is not valid.");
    }
    private ErrorOr<Answer> ValidateFieldOption(FormField field, SubscriptionAnswerRequest answer)
    {
        if (!field.OptionsSettings?.Options.Any(o => o.Value == answer.Answer) ?? false)
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is not valid.");

        return Answer.CreateOptions(answer.Answer);
    }
    private ErrorOr<Answer> ValidateFieldNumber(FormField field, SubscriptionAnswerRequest answer)
    {
        if (field.NumberSettings?.RequiredDigits is not null && answer.Answer.Length != field.NumberSettings.RequiredDigits)
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} does not have the required number of digits.");
        
        if (!long.TryParse(answer.Answer, out long value))
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is not a valid number.");

        if (field.NumberSettings?.Minimum is not null && field.NumberSettings.Minimum > value)
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is less then the minimum.");
        
        if (field.NumberSettings?.Maximum is not null && field.NumberSettings.Maximum < value)
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is more then the maximum.");

        return Answer.CreateNumber(answer.Answer);
    }
    private ErrorOr<Answer> ValidateFieldFile(FormField field, List<FileRequest> files)
    {
        var file = files.FirstOrDefault(f => f.Id?.ToLower() == field.Id.Value.ToString()?.ToLower());

        if(file is null)
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} does not seem to have a file.");

        if (field.FileSettings?.MaxSize is not null && file.file.Length > field.FileSettings?.MaxSize)
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} has a file too big.");

        if (field.FileSettings?.Extensions is not null && !field.FileSettings.Extensions.Contains(file.Type?.ToLower()))
            return Error.Validation("Subscription.InvalidField", $"The field with name {field.Description} is not a valid number.");

        return Answer.CreateFile(file.Name, file.file);
    }

}
