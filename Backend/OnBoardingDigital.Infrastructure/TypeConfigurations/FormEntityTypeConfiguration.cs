using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingDigital.Domain.Form;
using OnBoardingDigital.Domain.Form.Entities;
using OnBoardingDigital.Domain.Form.ValueObjects;

namespace OnBoardingDigital.Infrastructure.TypeConfigurations;

public class FormEntityTypeConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        ConfigureFormsTable(builder);
    }

    private void ConfigureFormsTable(EntityTypeBuilder<Form> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => FormId.Create(value));

        builder.Property(e => e.Name);

        builder.OwnsMany(e => e.Sections, sb => ConfigureFormSectionTable(sb));
    }

    private void ConfigureFormSectionTable(OwnedNavigationBuilder<Form, FormSection> builder)
    {
        builder.WithOwner().HasForeignKey("FormId");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => FormSectionId.Create(value));

        builder.Property(e => e.Name);

        builder.OwnsOne(e => e.Repeatable);
        builder.OwnsMany(e => e.Items, sb => ConfigureFormFieldTable(sb));
    }

    private void ConfigureFormFieldTable(OwnedNavigationBuilder<FormSection, FormField> builder)
    {
        builder.WithOwner().HasForeignKey("FormSectionId");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => FormFieldId.Create(value));

        builder.Property(e => e.Required);
        builder.Property(e => e.Order);
        builder.Property(e => e.Description);

        builder.OwnsOne(e => e.FileSettings);
        builder.OwnsOne(e => e.TextSettings);
        builder.OwnsOne(e => e.ChoiceSettings);
        builder.OwnsOne(e => e.OptionsSettings);
        builder.OwnsOne(e => e.NumberSettings);
        builder.OwnsOne(e => e.InformationSettings);
    }
}
