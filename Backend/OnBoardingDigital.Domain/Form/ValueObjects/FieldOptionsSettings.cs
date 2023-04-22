using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldOptionsSettings : ValueObject
{

    private FieldOptionsSettings()
    {
        //Todo: Validations
    }

    public static FieldOptionsSettings CreateNew() => new();

    protected override IEnumerable<object> GetEqualityComponents()
    {

    }
}
