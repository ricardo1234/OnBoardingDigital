using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
public sealed class Answer : ValueObject
{
    public FieldType FieldType { get; private set; }
    public string? TextValue { get; private set; }
    public bool? ChoiceValue { get; private set; }
    public string? OptionsValue { get; private set; }
    public long? NumberValue { get; private set; }
    public string? FileName { get; private set; }
    public byte[]? FileBytes { get; private set; }

    public string Value { get; private set; }

    private Answer(FieldType fieldType, string value, byte[]? fileBytes)
    {
        FieldType = fieldType ?? throw new ArgumentNullException(nameof(fieldType));
        Value = value ?? throw new ArgumentNullException(nameof(value)); 
        TextValue = null;
        ChoiceValue = null;
        OptionsValue = null;
        NumberValue = null;
        FileName = null;
        FileBytes = null;

        try
        {
            switch (FieldType.Id)
            {
                case ((int)AttributeFieldTypes.Number):
                    NumberValue = int.Parse(value);
                    break;
                case ((int)AttributeFieldTypes.Text):
                    TextValue = value;
                    break;
                case ((int)AttributeFieldTypes.Options):
                    OptionsValue = value;
                    break;
                case ((int)AttributeFieldTypes.Choice):
                    ChoiceValue = Boolean.Parse(value);
                    break;
                case ((int)AttributeFieldTypes.File):
                    FileName = value;
                    FileBytes = fileBytes;
                    break;
            }
        }
        catch
        {
            throw new ArgumentException($"The value ({Value}) is invalid for type {FieldType.Name}.");
        }
    }

    public static Answer CreateText(string value)
     => new(FieldType.Text, value, null);

    public static Answer CreateFile(string value, byte[] bytes)
    => new(FieldType.File, value, bytes);

    public static Answer CreateNumber(string value)
    => new(FieldType.Number, value, null);

    public static Answer CreateOptions(string value)
    => new(FieldType.Options, value, null);

    public static Answer CreateChoice(string value)
    => new(FieldType.Choice, value, null);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return FieldType;
    }
#pragma warning disable CS8618
    private Answer()
    {
    }
#pragma warning restore CS8618
}
