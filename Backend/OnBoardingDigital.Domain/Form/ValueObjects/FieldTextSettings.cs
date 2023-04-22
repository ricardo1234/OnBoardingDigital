using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldTextSettings : ValueObject
{
    private FieldTextSettings()
    {
        //Todo: Validations
    }

    public static FieldTextSettings CreateNew() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {

    }
}