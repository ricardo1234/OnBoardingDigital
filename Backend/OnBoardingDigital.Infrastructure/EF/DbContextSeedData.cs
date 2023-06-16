using Microsoft.EntityFrameworkCore.Query.Internal;
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
            if (context.Forms.AsEnumerable().SingleOrDefault(x => x.Id.Value.Equals(form.Id.Value)) is null)
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
    private static readonly FormId FormId = FormId.Create(Guid.Parse("b7a6c87c-553d-4a13-ab0d-b8d7f576e8c9"));
    private static readonly FormSectionId Information = FormSectionId.Create(Guid.Parse("e67e3b46-9eb8-4747-8373-ba3215571c29"));
    private static readonly FormSectionId SubscriptionType = FormSectionId.Create(Guid.Parse("cdaa4a23-25c4-4403-b9c0-6c6980659bff"));
    private static readonly FormSectionId PersonalIdentificiation= FormSectionId.Create(Guid.Parse("41fef0ea-535c-4775-a881-e09f716a0171"));
    private static readonly FormSectionId CompanyIdentificiation = FormSectionId.Create(Guid.Parse("60a8f804-a6fa-4eb8-895d-415d9dd85825"));
    private static readonly FormSectionId PersonalFiles = FormSectionId.Create(Guid.Parse("82624425-56f2-413f-a5ef-908ab7ff11d5"));
    private static readonly FormSectionId PersonalCompany = FormSectionId.Create(Guid.Parse("b4229780-4306-47df-be7a-779d32fec1cd"));
    private static readonly FormFieldId OptionField = FormFieldId.Create(Guid.Parse("2bd3a88c-3573-4880-88ae-4d8a14100d54"));
    #endregion

    private static Form GetFormExampleOne()
    {

        var form = Form.Create(FormId, "Contrato de Adesão", Information);

        var sectionInformation = FormSection.Create(Information, "Condições Gerais", 1, Repeatable.Create(), SubscriptionType);
        sectionInformation.AddFormField(
            FormField.CreateInformation(1, true, String.Empty, FieldInformationSettings.Create(@"<h1 style=""text-align:justify"">Condi&ccedil;&otilde;es Gerais do Contrato</h1>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>1.&ordf; </strong>A IFTHENPAY sendo entidade de pagamento regulada pelo Banco de Portugal obriga-se a fornecer ao Aderente servi&ccedil;os relacionados com a&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">disponibiliza&ccedil;&atilde;o, autoriza&ccedil;&atilde;o de utiliza&ccedil;&atilde;o e apoio de manuten&ccedil;&atilde;o de meios de pagamento, os quais ser&atilde;o utilizados por parte do Aderente, e os seus&nbsp;</span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">pr&oacute;prios clientes (doravante designados por consumidores), no &acirc;mbito da sua atividade independente.</span></span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>2.&ordf;</strong> Ap&oacute;s aprova&ccedil;&atilde;o da proposta subscrita pelo aderente, a IFTHENPAY disponibilizar&aacute; ao Aderente os elementos necess&aacute;rios &agrave; prossecu&ccedil;&atilde;o do fim&nbsp;</span></span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">definido nas condi&ccedil;&otilde;es particulares a que se destina o presente contrato, nomeadamente:</span></span></p>

<p style=""margin-left:40px; text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">a) Atribui&ccedil;&atilde;o duma chave de backoffice (&uacute;nica, pessoal e intransmiss&iacute;vel), a qual permitir&aacute; aceder a ferramentas de backoffice online;</span></span></p>

<p style=""margin-left:40px; text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">b) Acesso a uma aplica&ccedil;&atilde;o mobile desenvolvida e gerida pela IFTHENPAY;</span></span></p>

<p style=""margin-left:40px; text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">c) Exemplos de implementa&ccedil;&atilde;o do sistema fornecido pela IFTHENPAY em diversas plataformas online e em software, que poder&atilde;o ser&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">utilizados pelo Aderente.</span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>3.&ordf; </strong>Uma vez operacional, e os meios de pagamento contratualizados forem disponibilizados, a aplica&ccedil;&atilde;o inform&aacute;tica permitir&aacute; ao Aderente gerar os&nbsp;</span></span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">dados necess&aacute;rios ao pagamento desejado, correspondente a cada documento de cobran&ccedil;a que emita, agilizando desse modo o seu sistema de&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">recebimentos, podendo o Aderente apor tais indica&ccedil;&otilde;es nesses documentos.</span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>4.&ordf; </strong>As formas de pagamento fornecidas pela IFTHENPAY est&atilde;o associadas a uma conta da qual esta &eacute; titular, e todos os pagamentos efetuados por&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">esta via s&atilde;o dirigidos a essa conta banc&aacute;ria. Essa conta banc&aacute;ria &eacute; utilizada exclusivamente para os fins descritos neste contrato e os valores&nbsp;</span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">respeitantes aos pagamentos pertencentes aos aderentes n&atilde;o poder&atilde;o ser utilizados para qualquer outro fim. Para cumprir o requisito de separa&ccedil;&atilde;o de&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">fundos, essa conta banc&aacute;ria tem a men&ccedil;&atilde;o expressa de ser aberta &ldquo;por conta dos utilizadores deste servi&ccedil;o de pagamento&rdquo;.</span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>5.&ordf;</strong> A IFTHENPAY &eacute; alheia, e n&atilde;o poder&aacute; ser por qualquer modo responsabilizada, civil ou penalmente, por erros ocorridos na utiliza&ccedil;&atilde;o dos meios de&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">pagamento disponibilizados ao Aderente, nomeadamente resultantes de emiss&otilde;es erradas dos meios de pagamento por parte do Aderente, pagamento&nbsp;</span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">irregular ou indevido por parte do consumidor, sendo a emiss&atilde;o dos meios de pagamento contratualizados da exclusiva responsabilidade do Aderente.</span></span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>6.&ordm;</strong> Ser&aacute; tamb&eacute;m totalmente alheio &agrave; IFTHENPAY qualquer preju&iacute;zo resultante para o Aderente do uso indevido ou contr&aacute;rio aos procedimentos&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">institu&iacute;dos e/ou publicitados por aquela.</span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>7.&ordf;</strong> Relativamente aos pagamentos efetuados por parte do consumidor ao Aderente, pelos meios de pagamento contratualizados nos termos do presente&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">contrato, a IFTHENPAY far&aacute; a transfer&ecirc;ncia dos valores recebidos para a conta banc&aacute;ria indicada pelo Aderente, deduzidos do custo do servi&ccedil;o, nos&nbsp;</span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">per&iacute;odos contratados, tudo conforme descrito nas Condi&ccedil;&otilde;es Particulares.</span></span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>8.&ordf;</strong> O Aderente poder&aacute; ter acesso aos pagamentos efetuados pelos meios de pagamento contratualizados, habitualmente, exceto situa&ccedil;&otilde;es pontuais de&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">for&ccedil;a maior (sem necessidade de aviso pr&eacute;vio), imediatamente ap&oacute;s o pagamento (protocolo real-time). O referido acesso poder&aacute; ser feita:</span></p>

<p style=""margin-left:40px; text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">a) Via correio eletr&oacute;nico para o(s) endere&ccedil;o(s) profissional(ais) indicado(s) pelo Aderente nas condi&ccedil;&otilde;es particulares;</span></span></p>

<p style=""margin-left:40px; text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">b) Pela consulta no nosso website ou aplica&ccedil;&atilde;o mobile, com acesso restrito por Utilizador e C&oacute;digo de Acesso;</span></span></p>

<p style=""margin-left:40px; text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">c) Via Webservice e/ou Callback (caso o Aderente pretenda automatizar a leitura dos pagamentos).</span></span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>9.&ordf;</strong> Mensalmente a IFTHENPAY emitir&aacute; um extrato com a descri&ccedil;&atilde;o de todos os pagamentos do m&ecirc;s anterior, bem como uma fatura/recibo reportado ao&nbsp;</span></span><span style=""font-family:Calibri,sans-serif; font-size:11pt"">custo do servi&ccedil;o.</span></p>

<p style=""text-align:justify""><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif""><strong>10.&ordf;</strong> O Aderente vincula-se a n&atilde;o disponibilizar quaisquer servi&ccedil;os, bens ou conte&uacute;dos ilegais, ou quaisquer outros que violem ou sejam suscet&iacute;veis de&nbsp;</span></span><span style=""font-size:11pt""><span style=""font-family:Calibri,sans-serif"">violar a legisla&ccedil;&atilde;o em vigor, sendo exclusivamente respons&aacute;vel pela sua atividade e pela utiliza&ccedil;&atilde;o do servi&ccedil;o disponibilizado pela IFTHENPAY.</span></span></p>
"))
        );
        form.AddFormSection(sectionInformation);

        var sectionSubscriptionType = FormSection.Create(SubscriptionType, "Tipo de Cliente", 2, Repeatable.Create(), null);
        sectionSubscriptionType.AddMultipleFormFields(new() {
            FormField.Create(OptionField, 1, true, "Tipo Cliente", FieldType.Options,null,null,null, FieldOptionsSettings.Create(new()
            {
                FieldOptionObject.Create("Particular", "Particular", PersonalIdentificiation),
                FieldOptionObject.Create("Empresa", "Empresa", CompanyIdentificiation),
            }), null, null)
        });
        form.AddFormSection(sectionSubscriptionType);

        var sectionPersonalIdentificiation = FormSection.Create(PersonalIdentificiation, "Identificação Particular", 3, Repeatable.Create(), PersonalFiles);
        sectionPersonalIdentificiation.AddMultipleFormFields(new() {
             FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100,3)),
             FormField.CreateText(3, true, "Nº Telemóvel", FieldTextSettings.CreateWithValidation(15,9, "(\\+(9[976]\\d|8[987530]\\d|6[987]\\d|5[90]\\d|42\\d|3[875]\\d|2[98654321]\\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1)\\d{1,14})|\\d{9}$")),
             FormField.CreateText(4, true, "Email", FieldTextSettings.CreateWithValidation(100, 6, "^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\w{2,3})+$")),
             FormField.CreateText(5, true, "NIF", FieldTextSettings.Create(9,9)),
        });
        form.AddFormSection(sectionPersonalIdentificiation);

        var sectionCompanyIdentificiation = FormSection.Create(CompanyIdentificiation, "Identificação Empresa", 3, Repeatable.Create(true), PersonalCompany);
        sectionCompanyIdentificiation.AddMultipleFormFields(new() {
             FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100,3)),
             FormField.CreateText(3, true, "C.C.", FieldTextSettings.Create(8,8)),
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
