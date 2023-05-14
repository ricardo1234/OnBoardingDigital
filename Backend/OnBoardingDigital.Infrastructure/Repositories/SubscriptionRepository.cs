using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Infrastructure.EF;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.Infrastructure.Repositories;

public sealed class SubscriptionRepository : BaseRepository<Subscription, SubscriptionId>, ISubscriptionRepository
{
    public SubscriptionRepository(OnBoardingDigitalDbContext context) : base(context.Subscriptions)
    {
    }
}
