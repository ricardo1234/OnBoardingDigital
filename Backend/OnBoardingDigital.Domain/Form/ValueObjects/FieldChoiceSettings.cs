using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldChoiceSettings : ValueObject
{
    public string Group { get; }
    private FieldChoiceSettings()
    {
        //Todo: Validations
    }

    public static FieldChoiceSettings CreateNew() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Group;
    }
}
