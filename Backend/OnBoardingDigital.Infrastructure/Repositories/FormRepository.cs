using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Infrastructure.EF;

namespace OnBoardingDigital.Infrastructure.Repositories;

public sealed class FormRepository : BaseRepository<Form, FormId>, IFormRepository
{
    private readonly OnBoardingDigitalDbContext _context;

    public FormRepository(OnBoardingDigitalDbContext context) : base(context.Forms)
    {
        _context = context;
    }
}
