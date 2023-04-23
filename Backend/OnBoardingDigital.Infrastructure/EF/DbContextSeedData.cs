using OnBoardingDigital.Domain.Form;
using OnBoardingDigital.Domain.Form.Entities;
using OnBoardingDigital.Domain.Form.ValueObjects;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace OnBoardingDigital.Infrastructure.EF;

/// <summary>
/// Database seed data inialization class.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DbContextSeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>A task that is completed when the seed data has been added.</returns>
    public static async void SeedData(OnBoardingDigitalDbContext context)
    {
        _ = context ?? throw new ArgumentNullException($"{nameof(context)} cannot be null.");

        foreach (Form form in GetSeedDataForForms())
        {
            // only add warehouses that have not been registered yet
            if (context.Forms.SingleOrDefault(x => x.Id.Value.Equals(form.Id.Value)) is null)
                context.Forms.Add(form);
        }

        await context.SaveChangesAsync();
    }

    private static IList<Form> GetSeedDataForForms()
    {
        var seedDataDeliveries = new List<Form>();

        seedDataDeliveries.Add(GetFormExampleOne());

        return seedDataDeliveries;
    }

    #region SectionsIds
    private static readonly FormSectionId SubscriptionType = FormSectionId.CreateUnique();
    private static readonly FormSectionId PersonalIdentificiation= FormSectionId.CreateUnique();
    private static readonly FormSectionId CompanyIdentificiation = FormSectionId.CreateUnique();
    private static readonly FormSectionId PersonalFiles = FormSectionId.CreateUnique();
    private static readonly FormSectionId PersonalCompany = FormSectionId.CreateUnique();
    #endregion

    private static Form GetFormExampleOne()
    {
        var form = Form.Create("Contrato de Adesão");

        var sectionSubscriptionType = FormSection.Create(SubscriptionType, "Tipo de Cliente", 1, Repeatable.Create());
        sectionSubscriptionType.AddMultipleFormFields(new() {
            FormField.CreateChoice(1, true, "Particular", FieldChoiceSettings.Create("tipoCliente"), PersonalIdentificiation),
            FormField.CreateChoice(2, true, "Empresa", FieldChoiceSettings.Create("tipoCliente"), CompanyIdentificiation),
        });
        form.AddFormSection(sectionSubscriptionType);

        var sectionPersonalIdentificiation = FormSection.Create(PersonalIdentificiation, "Identificação Particular", 2, Repeatable.Create());
        sectionPersonalIdentificiation.AddMultipleFormFields(new() {
             FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100,3)),
             FormField.CreateDateTime(2, true, "Data Nascimento", FieldDateTimeSettings.CreateDate(isMaximumToday: true)),
             FormField.CreateText(3, true, "Nº Telemóvel", FieldTextSettings.CreateWithValidation(9,15, "(\\+(9[976]\\d|8[987530]\\d|6[987]\\d|5[90]\\d|42\\d|3[875]\\d|2[98654321]\\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\\d{1,14})|\\d{9}$")),
             FormField.CreateText(4, true, "Email", FieldTextSettings.CreateWithValidation(6,100, "^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$")),
             FormField.CreateText(5, true, "NIF", FieldTextSettings.Create(9,9)),
        });
        form.AddFormSection(sectionPersonalIdentificiation);

        var sectionCompanyIdentificiation = FormSection.Create(CompanyIdentificiation, "Identificação Empresa", 2, Repeatable.Create(true));
        sectionCompanyIdentificiation.AddMultipleFormFields(new() {
             FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100,3)),
             FormField.CreateDateTime(2, true, "Data Nascimento", FieldDateTimeSettings.CreateDate(isMaximumToday: true)),
             FormField.CreateText(3, true, "C.C.", FieldTextSettings.Create(8,8)),
             FormField.CreateDateTime(4, true, "Validade C.C.", FieldDateTimeSettings.CreateDate(isMinimumToday: true)),
             FormField.CreateText(5, true, "NIF", FieldTextSettings.Create(9,9)),
             FormField.CreateText(6, true, "Cargo", FieldTextSettings.Create()),
             FormField.CreateText(7, true, "Naturalidade", FieldTextSettings.Create()),
             FormField.CreateText(8, true, "Nacionalidade", FieldTextSettings.Create()),
             FormField.CreateChoice(9, true, "PEP - Pessoa Exposta Politicamente", FieldChoiceSettings.Create("pep")),
        });
        form.AddFormSection(sectionCompanyIdentificiation);

        var sectionPersonalFiles = FormSection.Create(PersonalFiles, "Ficheiros Particular", 3, Repeatable.Create());
        sectionPersonalFiles.AddMultipleFormFields(new() {
             FormField.CreateFile(1, true, "C.C ou Passport", FieldFileSettings.Create(new(){ "pdf", "png", "jpeg", "jpg" })),
             FormField.CreateFile(2, true, "Comprovativo IBAN", FieldFileSettings.Create(new(){ "pdf" })),
        });
        form.AddFormSection(sectionPersonalFiles);

        var sectionPersonalCompany = FormSection.Create(PersonalCompany, "Ficheiros Empresa", 3, Repeatable.Create());
        sectionPersonalCompany.AddMultipleFormFields(new() {
             FormField.CreateFile(1, true, "Comprovativo IBAN", FieldFileSettings.Create(new(){ "pdf" })),
        });
        form.AddFormSection(sectionPersonalCompany);

        return form;
    }

}
