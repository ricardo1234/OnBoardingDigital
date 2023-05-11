using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public sealed class FieldOptionObject : ValueObject
{
    public string Value { get; private set; }
    public string Text { get; private set; }
    public FormSectionId? NextSection { get; private set; }

    private FieldOptionObject(string value, string text, FormSectionId? nextSection)
    {
        //Todo: Validations
        Value = value;
        Text = text;
        NextSection = nextSection;
    }

    public static FieldOptionObject Create(string value, string text, FormSectionId? nextSection) => new(value, text, nextSection);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private FieldOptionObject()
    {
    }
#pragma warning restore CS8618
}
