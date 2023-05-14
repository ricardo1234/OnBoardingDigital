using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.Entities;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.FormAggregate;

namespace OnBoardingDigital.Infrastructure.TypeConfigurations;

public sealed class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
{
    private string _separator = "$§$";

    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        ConfigureFormsTable(builder);
    }

    private void ConfigureFormsTable(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => SubscriptionId.Create(value));

        builder.Property(e => e.FormId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                 nq => nq.Value,
                 value => FormId.Create(value));

        builder.Property(e => e.Email)
            .IsRequired();

        builder.OwnsMany(e => e.Answers, sb => ConfigureAnswersTable(sb));
        builder.Metadata.FindNavigation(nameof(Subscription.Answers))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureAnswersTable(OwnedNavigationBuilder<Subscription, SubscriptionAnswer> builder)
    {
        builder.WithOwner().HasForeignKey("SubscriptionId");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SubscriptionAnswerId.Create(value));

        builder.Property(e => e.FieldId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                 nq => nq.Value,
                 value => FormFieldId.Create(value));

        builder.Property(e => e.Index);

        builder.OwnsOne(e => e.Value, sb =>
        {
            sb.Property(e => e.FileName)
                .IsRequired(false);

            sb.Property(e => e.FileBytes)
                .IsRequired(false);

            sb.Property(e => e.OptionsValue)
                .IsRequired(false);

            sb.Property(e => e.NumberValue)
                .IsRequired(false);

            sb.Property(e => e.ChoiceValue)
                .IsRequired(false);

            sb.Property(e => e.TextValue)
                .IsRequired(false);

            sb.Property(e => e.Value)
                .IsRequired();

            sb.Property(e => e.FieldType)
                .ValueGeneratedNever()
                .HasConversion(
                   t => t.Id,
                   value => FieldType.From(value));
        });
    }
}
