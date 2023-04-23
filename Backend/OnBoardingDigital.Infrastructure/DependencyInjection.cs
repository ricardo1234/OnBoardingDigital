using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Infrastructure.EF;
using System.Diagnostics.CodeAnalysis;

namespace OnBoardingDigital.Infrastructure;

/// <summary>
/// Dependency injection module for the infrastructure project.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, string connetionString)
    {
        _ = services ?? throw new ArgumentNullException($"{nameof(services)} cannot be null");


        services.AddDbContext<OnBoardingDigitalDbContext>(opt =>
               opt.UseInMemoryDatabase(SchemaNames.OnBoardingDigital));

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        //Add Repository Dependencies Example:

        return services;
    }
}
