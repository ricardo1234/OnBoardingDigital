using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Form.Entities;
using System.Xml.Linq;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class Repeatable : ValueObject
{
    public bool CanRepeat { get; }
    public int? NumberOfReapeats { get; }

    private Repeatable(bool canRepeat, int? numberOfReapeats)
    {
        //Todo: Validations
        CanRepeat = canRepeat;
        NumberOfReapeats = numberOfReapeats;
    }

    public static Repeatable CreateNew(bool canRepeat = false, int? numberOfReapeats = null) => new(canRepeat, numberOfReapeats);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CanRepeat;
        yield return NumberOfReapeats;
    }
}