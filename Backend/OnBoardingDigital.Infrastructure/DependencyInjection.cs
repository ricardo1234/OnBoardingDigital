using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Infrastructure.EF;
using OnBoardingDigital.Infrastructure.Repositories;
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
               opt.UseSqlite(connetionString));

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        //Add Repository Dependencies Example:
        services.AddScoped<IFormRepository, FormRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        return services;
    }
}
