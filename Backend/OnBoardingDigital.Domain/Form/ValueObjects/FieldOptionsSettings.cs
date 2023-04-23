using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldOptionsSettings : ValueObject
{
    private List<string> _options;
    public IReadOnlyList<string> Options => _options.AsReadOnly();
    private FieldOptionsSettings(List<string> options)
    {
        //Todo: Validations
        _options = options;
    }

    public static FieldOptionsSettings Create(List<string> options) => new(options);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _options;
    }
#pragma warning disable CS8618
    private FieldOptionsSettings()
    {
    }
#pragma warning restore CS8618
}
