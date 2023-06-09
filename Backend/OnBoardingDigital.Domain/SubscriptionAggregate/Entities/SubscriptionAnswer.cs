using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.SubscriptionAggregate.Entities;

public sealed class SubscriptionAnswer: Entity<SubscriptionAnswerId>
{
    public FormFieldId FieldId { get; private set; }
    public int Index { get; private set; }
    public Answer Value { get; private set; }

    private SubscriptionAnswer(SubscriptionAnswerId id, FormFieldId fieldId, int index, Answer value) : base(id)
    {
        FieldId = fieldId;
        Index = index;
        Value = value;
    }

    public static SubscriptionAnswer CreateNew(FormFieldId fieldId, int index, Answer value) => new(SubscriptionAnswerId.CreateUnique(), fieldId, index, value);
    public static SubscriptionAnswer Create(SubscriptionAnswerId id, FormFieldId fieldId, int index, Answer value) => new(id, fieldId, index, value);

#pragma warning disable CS8618
    private SubscriptionAnswer()
    {
    }
#pragma warning restore CS8618

}
