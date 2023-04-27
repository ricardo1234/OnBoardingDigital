using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Infrastructure.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnBoardingDigitalDbContext _context;

    public UnitOfWork(OnBoardingDigitalDbContext context)
    {
        this._context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await this._context.SaveChangesAsync();
    }
}