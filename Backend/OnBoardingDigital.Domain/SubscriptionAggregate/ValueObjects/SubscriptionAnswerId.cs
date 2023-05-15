using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

public sealed class SubscriptionAnswerId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private SubscriptionAnswerId(Guid value)
    {
        Value = value;
    }

    public static SubscriptionAnswerId CreateUnique() => new SubscriptionAnswerId(Guid.NewGuid());

    public static SubscriptionAnswerId Create(Guid value) => new SubscriptionAnswerId(value);
    public static SubscriptionAnswerId CreateFromString(string value) => new SubscriptionAnswerId(Guid.Parse(value));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
