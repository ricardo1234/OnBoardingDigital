using OnBoardingDigital.Domain.Common;
using System.Reflection;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldFileSettings : ValueObject
{
    private List<string> _extensions;
    public IReadOnlyList<string> Extensions => _extensions.AsReadOnly();
    public long MaxSize { get; }
    private FieldFileSettings(List<string> extensions, long maxSize)
    {
        //Todo: Validations
        _extensions = extensions;
        MaxSize = maxSize;
    }

    public static FieldFileSettings CreateNew(List<string> extensions, long maxSize) => new(extensions, maxSize);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _extensions;
    }
}