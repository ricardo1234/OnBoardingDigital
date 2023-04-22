using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldFileSettings : ValueObject
{

    private FieldFileSettings()
    {
        //Todo: Validations
    }

    public static FieldFileSettings CreateNew() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {

    }
}