using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.Repositories;

public interface ISubscriptionRepository : IRepository<Subscription, SubscriptionId>
{
    public Task<List<Subscription>> GetByEmailAsync(string email);
}
