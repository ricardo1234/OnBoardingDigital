using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Form.ValueObjects;
using System.Reflection.Metadata;

namespace OnBoardingDigital.Domain.Form.Entities;

public sealed class FormField : Entity<FormFieldId>
{

    public int Order { get; }
    public bool Required { get; }
    public string Description { get; }
    public FieldType Type { get; }

    public FieldChoiceSettings ChoiceSettings { get; }
    public FieldFileSettings FileSettings { get; }
    public FieldNumberSettings NumberSettings { get; }
    public FieldOptionsSettings OptionsSettings { get; }
    public FieldTextSettings TextSettings { get; }
    public FieldInformationSettings InformationSettings { get; }

    private FormField(FormFieldId id, int order, bool required, string description, FieldType type, FieldChoiceSettings choiceSettings, FieldFileSettings fileSettings, FieldNumberSettings numberSettings, FieldOptionsSettings optionsSettings, FieldTextSettings textSettings, FieldInformationSettings informationSettings) : base(id)
    {
        Order = order;
        Required = required;
        Description = description;
        Type = type;
        ChoiceSettings = choiceSettings;
        FileSettings = fileSettings;
        NumberSettings = numberSettings;
        OptionsSettings = optionsSettings;
        TextSettings = textSettings;
        InformationSettings = informationSettings;
    }

    public static FormField Create(FormFieldId id, int order, bool required, string description, FieldType type, FieldChoiceSettings choiceSettings, FieldFileSettings fileSettings, FieldNumberSettings numberSettings, FieldOptionsSettings optionsSettings, FieldTextSettings textSettings, FieldInformationSettings informationSettings)
    {
        return new(id, order, required, description, type, choiceSettings, fileSettings, numberSettings, optionsSettings, textSettings, informationSettings);
    }

}