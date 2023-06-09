using OnBoardingDigital.Domain.FormAggregate;
using Microsoft.EntityFrameworkCore;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.Infrastructure.EF;

/// <summary>
/// Database context for OnBoardingDigital database.
/// </summary>
public sealed class OnBoardingDigitalDbContext : DbContext
{
    /// <summary>
    /// Form entities.
    /// </summary>
    public DbSet<Form> Forms { get; set; }

    /// <summary>
    /// Subscription entities.
    /// </summary>
    public DbSet<Subscription> Subscriptions { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="OnBoardingDigitalDbContext"/>
    /// </summary>
    /// <param name="options">The database context options.</param>
    public OnBoardingDigitalDbContext(DbContextOptions options) : base(options) {
        this.Database.EnsureCreated();
    }

    /// <summary>
    /// Actions to be taken when model creation is being performed.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnBoardingDigitalDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}