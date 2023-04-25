using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class Repeatable : ValueObject
{
    public bool CanRepeat { get; private set; }
    public int? NumberOfReapeats { get; private set; }

    private Repeatable(bool canRepeat, int? numberOfReapeats)
    {
        //Todo: Validations
        CanRepeat = canRepeat;
        NumberOfReapeats = numberOfReapeats;
    }

    public static Repeatable Create(bool canRepeat = false, int? numberOfReapeats = null) => new(canRepeat, numberOfReapeats);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CanRepeat;
        yield return NumberOfReapeats;
    }

#pragma warning disable CS8618
    private Repeatable()
    {
    }
#pragma warning restore CS8618
}