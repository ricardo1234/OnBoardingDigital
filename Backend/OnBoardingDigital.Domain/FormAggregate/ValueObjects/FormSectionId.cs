using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class FormSectionId : ValueObject
{
    public Guid Value { get; }

    private FormSectionId(Guid value)
    {
        Value = value;
    }

    public static FormSectionId CreateUnique() => new FormSectionId(Guid.NewGuid());

    public static FormSectionId Create(Guid value) => new FormSectionId(value);
    public static FormSectionId CreateFromString(string value) => new FormSectionId(Guid.Parse(value));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}