using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using System.Text.Json;

namespace OnBoardingDigital.Infrastructure.TypeConfigurations;

public class FormEntityTypeConfiguration : IEntityTypeConfiguration<Form>
{
    private string _separator = "$§$";

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

        builder.Property(e => e.FirstSection)
            .IsRequired()
            .ValueGeneratedNever()
            .HasConversion(
                 nq => nq.Value,
                 value => FormSectionId.Create(value));

        builder.Property(e => e.Name);

        builder.OwnsMany(e => e.Sections, sb => ConfigureFormSectionTable(sb));
        builder.Metadata.FindNavigation(nameof(Form.Sections))!.SetPropertyAccessMode(PropertyAccessMode.Field);
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

        builder.Property(e => e.DefaultNextSection)
            .IsRequired(false)
            .ValueGeneratedNever()
            .HasConversion(
                 nq => nq.Value,
                 value => FormSectionId.Create(value));

        builder.Property(e => e.Name);

        builder.OwnsOne(e => e.Repeatable);

        builder.OwnsMany(e => e.Fields, sb => ConfigureFormFieldTable(sb));
        builder.Navigation(e => e.Fields).Metadata.SetField("_fields");
        builder.Navigation(e => e.Fields).UsePropertyAccessMode(PropertyAccessMode.Field);
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

        builder.Property(e => e.Type)
           .ValueGeneratedNever()
           .HasConversion(
               t => t.Id,
               value => FieldType.From(value));

        //builder.Property(e => e.NextSection)
        //    .IsRequired(false)
        //    .ValueGeneratedNever()
        //    .HasConversion(
        //     nq => nq.Value,
        //     value => FormSectionId.Create(value));

        builder.OwnsOne(e => e.FileSettings, fb =>
        {
            fb.Property(e => e.Extensions)
            .HasConversion(
                e => string.Join(_separator, e),
                value => value.Split(_separator, StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        });
        builder.OwnsOne(e => e.TextSettings);
        builder.OwnsOne(e => e.ChoiceSettings, cb =>
        {
            cb.Property(e => e.NextSection)
            .IsRequired(false)
            .ValueGeneratedNever()
            .HasConversion(
                 nq => nq.Value,
                 value => FormSectionId.Create(value));
        });
        builder.OwnsOne(e => e.NumberSettings);
        builder.OwnsOne(e => e.OptionsSettings, ob =>
        {
            ob.OwnsMany(e => e.Options, ovb => {
                ovb.Property(e => e.NextSection)
                .IsRequired(false)
                .ValueGeneratedNever()
                .HasConversion(
                     nq => nq.Value,
                     value => FormSectionId.Create(value));
            });
            ob.Navigation(e => e.Options).Metadata.SetField("_options");
            ob.Navigation(e => e.Options).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.OwnsOne(e => e.InformationSettings);
    }
}
