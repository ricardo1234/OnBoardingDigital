using OnBoardingDigital.Infrastructure;
using OnBoardingDigital.Infrastructure.EF;
using System.Diagnostics.CodeAnalysis;

namespace OnBoardingDigital.API.Infrastructure;


/// <summary>
/// Extension class to seed data on database.
/// </summary>
[ExcludeFromCodeCoverage]
public static class SeedDataExtension
{
    /// <summary>
    /// Seeds the data for the database.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder SeedData(this IApplicationBuilder app)
    {
        _ = app ?? throw new ArgumentNullException($"{nameof(app)} cannot be null.");

        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<OnBoardingDigitalDbContext>();
            DbContextSeedData.SeedData(context);
        }
        catch (Exception ex)
        {

        }

        return app;
    }
}

