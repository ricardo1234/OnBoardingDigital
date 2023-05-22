using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Infrastructure.EF;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace OnBoardingDigital.Infrastructure.Repositories;

public sealed class SubscriptionRepository : ISubscriptionRepository
{
    private OnBoardingDigitalDbContext _context;
    public SubscriptionRepository(OnBoardingDigitalDbContext context)
    {
        _context = context;
    }
    public async Task<List<Subscription>> GetAllAsync()
    {
        return await _context.Subscriptions.ToListAsync();
    }

    public async Task<Subscription?> GetByIdAsync(SubscriptionId id)
    {
        var list = await GetAllAsync();
        return list.Where(x => id.Equals(x.Id)).FirstOrDefault();
    }
    public async Task<List<Subscription>> GetByIdsAsync(List<SubscriptionId> ids)
    {
        var list = await GetAllAsync();
        return list.Where(x => ids.Contains(x.Id)).ToList();
    }
    public async Task<Subscription> AddAsync(Subscription obj)
    {
        var ret = await _context.Subscriptions.AddAsync(obj);
        return ret.Entity;
    }

    public void Remove(Subscription obj)
    {
        _context.Subscriptions.Remove(obj);
    }

    public async Task<List<Subscription>> GetByEmailAsync(string email)
    {
        var list = await GetAllAsync();
        return list.Where(s => s.Email == email).ToList();
    }
}
