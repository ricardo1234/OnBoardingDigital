using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

public class SubscriptionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SubscriptionId(Guid value)
    {
        Value = value;
    }

    public static SubscriptionId CreateUnique() => new SubscriptionId(Guid.NewGuid());

    public static SubscriptionId Create(Guid value) => new SubscriptionId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}