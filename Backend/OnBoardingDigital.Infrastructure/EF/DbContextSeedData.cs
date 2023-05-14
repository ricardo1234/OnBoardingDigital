using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using System.Diagnostics.CodeAnalysis;

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
    private static readonly FormId FormId= FormId.Create(Guid.Parse("b7a6c87c-553d-4a13-ab0d-b8d7f576e8c9"));
    private static readonly FormSectionId Information = FormSectionId.CreateUnique();
    private static readonly FormSectionId SubscriptionType = FormSectionId.CreateUnique();
    private static readonly FormSectionId PersonalIdentificiation= FormSectionId.CreateUnique();
    private static readonly FormSectionId CompanyIdentificiation = FormSectionId.CreateUnique();
    private static readonly FormSectionId PersonalFiles = FormSectionId.CreateUnique();
    private static readonly FormSectionId PersonalCompany = FormSectionId.CreateUnique();
    #endregion

    private static Form GetFormExampleOne()
    {

        var form = Form.Create(FormId, "Contrato de Adesão", Information);

        var sectionInformation = FormSection.Create(Information, "Condições Gerais", 1, Repeatable.Create(), SubscriptionType);
        sectionInformation.AddFormField(
            FormField.CreateInformation(1, true, String.Empty, FieldInformationSettings.Create(@"<h1 style=""color: #5e9ca0;"">You can edit <span style=""color: #2b2301;"">this demo</span> text!</h1>
<h2 style=""color: #2e6c80;"">How to use the editor:</h2>
<p>Paste your documents in the visual editor on the left or your HTML code in the source editor in the right. <br />Edit any of the two areas and see the other changing in real time.&nbsp;</p>
<p>Click the <span style=""background-color: #2b2301; color: #fff; display: inline-block; padding: 3px 10px; font-weight: bold; border-radius: 5px;"">Clean</span> button to clean your source code.</p>
<p><strong>Save this link into your bookmarks and share it with your friends. It is all FREE! </strong><br /><strong>Enjoy!</strong></p>
<p><strong>&nbsp;</strong></p>"))
        );
        form.AddFormSection(sectionInformation);

        var sectionSubscriptionType = FormSection.Create(SubscriptionType, "Tipo de Cliente", 2, Repeatable.Create(), null);
        sectionSubscriptionType.AddMultipleFormFields(new() {
            FormField.CreateOptions(1, true, "Tipo Cliente", FieldOptionsSettings.Create(new()
            {
                FieldOptionObject.Create("Particular", "Particular", PersonalIdentificiation),
                FieldOptionObject.Create("Empresa", "Empresa", CompanyIdentificiation),
            }))
        });
        form.AddFormSection(sectionSubscriptionType);

        var sectionPersonalIdentificiation = FormSection.Create(PersonalIdentificiation, "Identificação Particular", 3, Repeatable.Create(), PersonalFiles);
        sectionPersonalIdentificiation.AddMultipleFormFields(new() {
             FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100,3)),
             FormField.CreateDateTime(2, true, "Data Nascimento", FieldDateTimeSettings.CreateDate(isMaximumToday: true)),
             FormField.CreateText(3, true, "Nº Telemóvel", FieldTextSettings.CreateWithValidation(15,9, "(\\+(9[976]\\d|8[987530]\\d|6[987]\\d|5[90]\\d|42\\d|3[875]\\d|2[98654321]\\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\\d{1,14})|\\d{9}$")),
             FormField.CreateText(4, true, "Email", FieldTextSettings.CreateWithValidation(100, 6, "^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$")),
             FormField.CreateText(5, true, "NIF", FieldTextSettings.Create(9,9)),
        });
        form.AddFormSection(sectionPersonalIdentificiation);

        var sectionCompanyIdentificiation = FormSection.Create(CompanyIdentificiation, "Identificação Empresa", 3, Repeatable.Create(true), PersonalCompany);
        sectionCompanyIdentificiation.AddMultipleFormFields(new() {
             FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100,3)),
             FormField.CreateDateTime(2, true, "Data Nascimento", FieldDateTimeSettings.CreateDate(isMaximumToday: true)),
             FormField.CreateText(3, true, "C.C.", FieldTextSettings.Create(8,8)),
             FormField.CreateDateTime(4, true, "Validade C.C.", FieldDateTimeSettings.CreateDate(isMinimumToday: true)),
             FormField.CreateText(5, true, "NIF", FieldTextSettings.Create(9,9)),
             FormField.CreateText(6, true, "Cargo", FieldTextSettings.Create()),
             FormField.CreateText(7, true, "Naturalidade", FieldTextSettings.Create()),
             FormField.CreateText(8, true, "Nacionalidade", FieldTextSettings.Create()),
             FormField.CreateChoice(9, false, "PEP - Pessoa Exposta Politicamente", FieldChoiceSettings.Create("pep", null)),
        });
        form.AddFormSection(sectionCompanyIdentificiation);

        var sectionPersonalFiles = FormSection.Create(PersonalFiles, "Ficheiros Particular", 4, Repeatable.Create(), null);
        sectionPersonalFiles.AddMultipleFormFields(new() {
             FormField.CreateFile(1, true, "C.C ou Passport", FieldFileSettings.Create(new(){ "pdf", "png", "jpeg", "jpg" })),
             FormField.CreateFile(2, true, "Comprovativo IBAN", FieldFileSettings.Create(new(){ "pdf" })),
        });
        form.AddFormSection(sectionPersonalFiles);

        var sectionPersonalCompany = FormSection.Create(PersonalCompany, "Ficheiros Empresa", 4, Repeatable.Create(), null);
        sectionPersonalCompany.AddMultipleFormFields(new() {
             FormField.CreateFile(1, true, "Comprovativo IBAN", FieldFileSettings.Create(new(){ "pdf" })),
        });
        form.AddFormSection(sectionPersonalCompany);

        return form;
    }

}
