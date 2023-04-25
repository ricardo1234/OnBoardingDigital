using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class FormId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private FormId(Guid value)
    {
        Value = value;
    }

    public static FormId CreateUnique() => new FormId(Guid.NewGuid());

    public static FormId Create(Guid value) => new FormId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}