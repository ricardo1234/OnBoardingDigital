using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldNumberSettings : ValueObject
{

    private FieldNumberSettings()
    {
        //Todo: Validations
    }

    public static FieldNumberSettings CreateNew() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {

    }
}