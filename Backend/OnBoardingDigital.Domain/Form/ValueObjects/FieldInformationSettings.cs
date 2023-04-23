using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldInformationSettings : ValueObject
{
    public string HtmlValue { get; private set; }
    private FieldInformationSettings(string htmlValue)
    {
        //Todo: Validations
        HtmlValue = htmlValue;
    }

    public static FieldInformationSettings CreateNew(string htmlValue) => new(htmlValue);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return HtmlValue;
    }
#pragma warning disable CS8618
    private FieldInformationSettings()
    {
    }
#pragma warning restore CS8618
}