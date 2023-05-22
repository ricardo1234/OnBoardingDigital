using Microsoft.EntityFrameworkCore;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Infrastructure.EF;

namespace OnBoardingDigital.Infrastructure.Repositories;

public sealed class FormRepository : IFormRepository
{
    private readonly OnBoardingDigitalDbContext _context;

    public FormRepository(OnBoardingDigitalDbContext context)
    {
        _context = context;
    }

    public async Task<List<Form>> GetAllAsync()
    {
        return await _context.Forms.ToListAsync();
    }

    public async Task<Form?> GetByIdAsync(FormId id)
    {
        var list = await GetAllAsync();
        return list.Where(x => id.Equals(x.Id)).FirstOrDefault();
    }
    public async Task<List<Form>> GetByIdsAsync(List<FormId> ids)
    {
        var list = await GetAllAsync();
        return list.Where(x => ids.Contains(x.Id)).ToList();
    }
    public async Task<Form> AddAsync(Form obj)
    {
        var ret = await _context.Forms.AddAsync(obj);
        return ret.Entity;
    }

    public void Remove(Form obj)
    {
        _context.Forms.Remove(obj);
    }

    public async Task<bool> FormExistsAsync(FormId id)
    {
        var list = await GetAllAsync();
        return list.Any(c => c.Id.Equals(id));
    }
}
