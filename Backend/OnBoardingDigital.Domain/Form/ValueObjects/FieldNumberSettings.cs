using Microsoft.VisualBasic;
using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldNumberSettings : ValueObject
{
    public int Minimum { get; private set; }
    public int Maximum { get; private set; }
    public int RequiredDigits { get; private set; }
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
#pragma warning disable CS8618
    private FieldNumberSettings()
    {
    }
#pragma warning restore CS8618
}