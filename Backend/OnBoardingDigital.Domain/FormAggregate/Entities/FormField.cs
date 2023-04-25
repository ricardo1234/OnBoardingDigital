using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.FormAggregate.Entities;

public sealed class FormField : Entity<FormFieldId>
{
    public FormSectionId? NextQuestion { get; private set; }
    public int Order { get; private set; }
    public bool Required { get; private set; }
    public string Description{ get; private set; }
    public FieldType Type { get; private set; }

    public FieldChoiceSettings? ChoiceSettings { get; private set; }
    public FieldFileSettings? FileSettings { get; private set; }
    public FieldNumberSettings? NumberSettings { get; private set; }
    public FieldOptionsSettings? OptionsSettings { get; private set; }
    public FieldTextSettings? TextSettings { get; private set; }
    public FieldInformationSettings? InformationSettings { get; private set; }
    public FieldDateTimeSettings? DateTimeSettings { get; private set; }

    private FormField(FormFieldId id, int order, bool required, string description, FormSectionId? nextQuestion, FieldType type, FieldChoiceSettings? choiceSettings, FieldFileSettings? fileSettings, FieldNumberSettings? numberSettings, FieldOptionsSettings? optionsSettings, FieldTextSettings? textSettings, FieldInformationSettings? informationSettings, FieldDateTimeSettings? dateTimeSettings) : base(id)
    {
        Order = order;
        Required = required;
        Type = type;
        Description = description;
        NextQuestion = nextQuestion;
        ChoiceSettings = choiceSettings;
        FileSettings = fileSettings;
        NumberSettings = numberSettings;
        OptionsSettings = optionsSettings;
        TextSettings = textSettings;
        InformationSettings = informationSettings;
        DateTimeSettings = dateTimeSettings;
    }

    public static FormField CreateChoice(int order, bool required, string description, FieldChoiceSettings settings, FormSectionId? nextQuestion = null)
    => new(FormFieldId.CreateUnique(), order, required, description, nextQuestion, FieldType.Choice, settings, null, null, null, null, null, null);
    
    public static FormField CreateFile(int order, bool required, string description, FieldFileSettings settings)
    => new(FormFieldId.CreateUnique(), order, required, description, null, FieldType.File, null, settings, null, null, null, null, null);
    
    public static FormField CreateNumber(int order, bool required, string description, FieldNumberSettings settings)
    => new (FormFieldId.CreateUnique(), order, required, description, null, FieldType.Number, null, null, settings, null, null, null, null);
    
    public static FormField CreateOptions(int order, bool required, string description, FieldOptionsSettings settings)
    => new(FormFieldId.CreateUnique(), order, required, description, null, FieldType.Options, null, null, null, settings, null, null, null);
    
    public static FormField CreateText(int order, bool required, string description, FieldTextSettings settings)
    => new(FormFieldId.CreateUnique(), order, required, description, null, FieldType.Text, null, null, null, null, settings, null, null);

    public static FormField CreateInformation(int order, bool required, string description, FieldInformationSettings settings)
    => new(FormFieldId.CreateUnique(), order, required, description, null, FieldType.Information, null, null, null, null, null, settings, null);

    public static FormField CreateDateTime(int order, bool required, string description, FieldDateTimeSettings settings)
   => new(FormFieldId.CreateUnique(), order, required, description, null, FieldType.DateTime, null, null, null, null, null, null, settings);

#pragma warning disable CS8618
    private FormField()
    {
    }
#pragma warning restore CS8618
}