using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class FieldChoiceSettings : ValueObject
{
    public string Group { get; private set; }
    public FormSectionId? NextSection { get; private set; }

    private FieldChoiceSettings(string group, FormSectionId? nextSection)
    {
        //Todo: Validations
        Group = group;
        NextSection = nextSection;
    }

    public static FieldChoiceSettings Create(string group, FormSectionId? nextSection) => new(group, nextSection);

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
