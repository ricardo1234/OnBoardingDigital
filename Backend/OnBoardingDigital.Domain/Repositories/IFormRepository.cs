using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.Domain.Repositories;

public interface IFormRepository : IRepository<Form, FormId>
{
    Task<bool> FormExistsAsync(FormId id);
}
