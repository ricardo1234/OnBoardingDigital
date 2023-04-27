namespace OnBoardingDigital.Domain.Common;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}