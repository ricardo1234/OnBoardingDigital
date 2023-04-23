using OnBoardingDigital.Domain.Common;
using System.Reflection;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldFileSettings : ValueObject
{
    private List<string> _extensions;
    public IReadOnlyList<string> Extensions => _extensions.AsReadOnly();
    public long? MaxSize { get; private set; }
    private FieldFileSettings(List<string> extensions, long? maxSize)
    {
        //Todo: Validations
        _extensions = extensions;
        MaxSize = maxSize;
    }

    public static FieldFileSettings Create(List<string> extensions, long? maxSize = null) => new(extensions, maxSize);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _extensions;
    }
#pragma warning disable CS8618
    private FieldFileSettings()
    {
    }
#pragma warning restore CS8618
}