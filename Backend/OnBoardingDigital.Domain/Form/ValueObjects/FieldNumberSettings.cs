using Microsoft.VisualBasic;
using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldNumberSettings : ValueObject
{
    public int Minimum { get; }
    public int Maximum { get; }
    public int RequiredDigits { get; }
    private FieldNumberSettings(int minimum, int maximum, int requiredDigits)
    {
        //Todo: Validations
        Minimum = minimum;
        Maximum = maximum;
        RequiredDigits = requiredDigits;
    }

    public static FieldNumberSettings CreateNew(int minimum, int maximum, int requiredDigits) => new(minimum, maximum, requiredDigits);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Maximum;
        yield return Minimum;
        yield return RequiredDigits;
    }
}