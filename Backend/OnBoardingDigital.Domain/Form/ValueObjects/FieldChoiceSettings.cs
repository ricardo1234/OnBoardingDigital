using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldChoiceSettings : ValueObject
{
    public string Group { get; private set; }
    private FieldChoiceSettings(string group)
    {
        //Todo: Validations
        Group = group;
    }

    public static FieldChoiceSettings Create(string group) => new(group);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Group;
    }
#pragma warning disable CS8618
    private FieldChoiceSettings()
    {
    }
#pragma warning restore CS8618
}
