using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Infrastructure.EF;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace OnBoardingDigital.Infrastructure.Repositories;

public sealed class SubscriptionRepository : BaseRepository<Subscription, SubscriptionId>, ISubscriptionRepository
{
    private OnBoardingDigitalDbContext _context;
    public SubscriptionRepository(OnBoardingDigitalDbContext context) : base(context.Subscriptions)
    {
        _context = context;
    }

    public async Task<List<Subscription>> GetByEmailAsync(string email)
    {
        return await _context.Subscriptions.Where(s => s.Email == email).ToListAsync();
    }
}
