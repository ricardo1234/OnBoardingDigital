using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldChoiceSettings : ValueObject
{

    private FieldChoiceSettings()
    {
        //Todo: Validations
    }

    public static FieldChoiceSettings CreateNew() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {

    }
}
