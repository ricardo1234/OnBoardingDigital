using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FormFieldId : ValueObject
{
    public Guid Value { get; }

    private FormFieldId(Guid value)
    {
        Value = value;
    }

    public static FormFieldId CreateUnique() => new FormFieldId(Guid.NewGuid());

    public static FormFieldId Create(Guid value) => new FormFieldId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}