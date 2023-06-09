using ErrorOr;
using MediatR;
using OnBoardingDigital.API.Application.Commands.Subscriptions;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using System.Xml.Serialization;
using OnBoardingDigital.Infrastructure.Repositories;
using System.Diagnostics;

namespace OnBoardingDigital.API.Application.Commands.Forms
{
    public class ChangeFormCommandHandler : IRequestHandler<ChangeFormCommand, ErrorOr<Form>>
    {
        private readonly IFormRepository formRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChangeFormCommandHandler(IFormRepository formRepository, IUnitOfWork unitOfWork)
        {
            this.formRepository = formRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Form>> Handle(ChangeFormCommand request, CancellationToken cancellationToken)
        {

            var formId = FormId.CreateFromString(request.Id);

            var form = await formRepository.GetByIdAsync(formId);
            if (form is null)
                return Error.NotFound("Form.NotFound", "Form was not found.");

            var section = form.Sections.FirstOrDefault(s => s.Id.Equals(FormSectionId.Create(Guid.Parse("cdaa4a23-25c4-4403-b9c0-6c6980659bff"))));

            if (section is null)
                return Error.NotFound("Section.NotFound", "Section was not found.");

            var field = section.Fields.FirstOrDefault(s => s.Id.Equals(FormFieldId.Create(Guid.Parse("2bd3a88c-3573-4880-88ae-4d8a14100d54"))));
            
            if (field is null)
                return Error.NotFound("FormField.NotFound", "Field was not found.");

            var sectionId = FormSectionId.CreateUnique();

            field.OptionsSettings.AddOption(FieldOptionObject.Create(
                "Random Option " + sectionId.Value.ToString()[0], 
                "Random Text " + sectionId.Value.ToString()[0],
                sectionId
            ));

            form.AddFormSection(CreateRandomSection(sectionId));

            await unitOfWork.CommitAsync();

            return form;
        }

        private FormSection CreateRandomSection(FormSectionId id)
        {
            var sectionPersonalIdentificiation = FormSection.Create(id, "Random Section " + id.Value.ToString()[0] , 3, Repeatable.Create(), null);
            sectionPersonalIdentificiation.AddMultipleFormFields(GetRandomFields());
            return sectionPersonalIdentificiation;
        }

        private List<FormField> GetRandomFields()
        {
            var random = new Random();
            int n = random.Next(1, 7);

            List<FormField> fieldsAvailable = new ()
            {
                    FormField.CreateText(1, true, "Nome", FieldTextSettings.Create(100, 3)),
                    FormField.CreateText(3, true, "C.C.", FieldTextSettings.Create(8, 8)),
                    FormField.CreateText(5, true, "NIF", FieldTextSettings.Create(9, 9)),
                    FormField.CreateText(6, true, "Cargo", FieldTextSettings.Create()),
                    FormField.CreateText(7, true, "Naturalidade", FieldTextSettings.Create()),
                    FormField.CreateText(8, true, "Nacionalidade", FieldTextSettings.Create()),
                    FormField.CreateChoice(9, false, "PEP - Pessoa Exposta Politicamente", FieldChoiceSettings.Create("pep", null)),
            };

            var result = new List<FormField>();

            for (int i = 0; i < n; i++)
            {
                var rnd = random.Next(0 , fieldsAvailable.Count-1);
                result.Add(
                    fieldsAvailable[rnd]);
                fieldsAvailable.RemoveAt(rnd);
            }

            return result;
        }
    }
}
