using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

public class Report : ValueObject
{
    public SubscriptionId SubscriptionId { get; private set; }
    public byte[] FileBytes { get; private set; }
    public string FileName { get; private set; }
    public DateTime CreatedAtUtc { get; set; }

    private Report(SubscriptionId subscriptionId, byte[] fileBytes, string fileName)
    {
        SubscriptionId = subscriptionId;
        FileBytes = fileBytes;
        FileName = fileName;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public static Report Create(SubscriptionId subscriptionId, byte[] fileBytes, string fileName)
    => new(subscriptionId, fileBytes, fileName);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return SubscriptionId;
        yield return CreatedAtUtc;
    }
#pragma warning disable CS8618
    private Report()
    {
    }
#pragma warning restore CS8618
}