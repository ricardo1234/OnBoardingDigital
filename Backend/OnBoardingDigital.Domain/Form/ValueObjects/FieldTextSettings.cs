using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldTextSettings : ValueObject
{
    public int? CharMaximun { get; }
    public int? CharMinimum { get; }
    public string ValidationExpression { get; }
    private FieldTextSettings(int? charMaximun, int? charMinimum, string validationExpression)
    {
        //Todo: Validations
        CharMaximun = charMaximun;
        CharMinimum = charMinimum;
        ValidationExpression = validationExpression;
    }

    public static FieldTextSettings CreateNew(int? charMaximun, int? charMinimum, string validationExpression) => new(charMaximun, charMinimum, validationExpression);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CharMaximun;
        yield return CharMinimum;
        yield return ValidationExpression;
    }
}