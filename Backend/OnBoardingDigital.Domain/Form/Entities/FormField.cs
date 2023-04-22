using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Form.ValueObjects;
using System.Reflection.Metadata;

namespace OnBoardingDigital.Domain.Form.Entities;

public sealed class FormField : Entity<FormFieldId>
{

    public int Order { get; }
    public bool Required { get; }
    public string Description { get; }
    public FieldType Type { get; set; }

    public FieldChoiceSettings ChoiceSettings { get; set; }
    public FieldFileSettings FileSettings { get; set; }
    public FieldNumberSettings NumberSettings { get; set; }
    public FieldOptionsSettings OptionsSettings { get; set; }
    public FieldTextSettings TextSettings { get; set; }
    
    private FormField(FormFieldId id, int order, bool required, string description, FieldType type, FieldChoiceSettings choiceSettings, FieldFileSettings fileSettings, FieldNumberSettings numberSettings, FieldOptionsSettings optionsSettings, FieldTextSettings textSettings) : base(id)
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
    }

    public static FormField Create(FormFieldId id, int order, bool required, string description, FieldType type, FieldChoiceSettings choiceSettings, FieldFileSettings fileSettings, FieldNumberSettings numberSettings, FieldOptionsSettings optionsSettings, FieldTextSettings textSettings)
    {
        return new(id, order, required, description, type, choiceSettings, fileSettings, numberSettings, optionsSettings, textSettings);
    }

}