using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class FieldTextSettings : ValueObject
{
    public int? CharMaximum { get; private set; }
    public int? CharMinimum { get; private set; }
    public string? ValidationExpression { get; private set; }
    private FieldTextSettings(int? charMaximum, int? charMinimum, string? validationExpression)
    {
        //Todo: Validations
        CharMaximum = charMaximum;
        CharMinimum = charMinimum;
        ValidationExpression = validationExpression;
    }

    public static FieldTextSettings Create(int? charMaximum = null, int? charMinimum = null) => new(charMaximum, charMinimum, null);
    public static FieldTextSettings CreateWithValidation(int? charMaximum, int? charMinimum, string validationExpression) => new(charMaximum, charMinimum, validationExpression);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CharMaximum;
        yield return CharMinimum;
        yield return ValidationExpression;
    }
#pragma warning disable CS8618
    private FieldTextSettings()
    {
    }
#pragma warning restore CS8618
}