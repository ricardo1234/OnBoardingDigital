using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class FieldOptionsSettings : ValueObject
{
    private List<FieldOptionObject> _options;
    public IReadOnlyList<FieldOptionObject> Options => _options.AsReadOnly();
    private FieldOptionsSettings(List<FieldOptionObject> options)
    {
        //Todo: Validations
        _options = options;
    }

    public static FieldOptionsSettings Create(List<FieldOptionObject> options) => new(options);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _options;
    }
    public void AddOption(FieldOptionObject option)
    {
        _options.Add(option);
    }


#pragma warning disable CS8618
    private FieldOptionsSettings()
    {
    }
#pragma warning restore CS8618
}
