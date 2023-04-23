using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldTextSettings : ValueObject
{
    public int? CharMaximun { get; private set; }
    public int? CharMinimum { get; private set; }
    public string? ValidationExpression { get; private set; }
    private FieldTextSettings(int? charMaximun, int? charMinimum, string? validationExpression)
    {
        //Todo: Validations
        CharMaximun = charMaximun;
        CharMinimum = charMinimum;
        ValidationExpression = validationExpression;
    }

    public static FieldTextSettings Create(int? charMaximun = null, int? charMinimum = null) => new(charMaximun, charMinimum, null);
    public static FieldTextSettings CreateWithValidation(int? charMaximun, int? charMinimum, string validationExpression) => new(charMaximun, charMinimum, validationExpression);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CharMaximun;
        yield return CharMinimum;
        yield return ValidationExpression;
    }
#pragma warning disable CS8618
    private FieldTextSettings()
    {
    }
#pragma warning restore CS8618
}