using Mapster;
using OnBoardingDigital.Contracts.Form;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.Entities;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;

namespace OnBoardingDigital.API.Mapping;

public class FormMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        FormResponse(config);
    }

    private void FormResponse(TypeAdapterConfig config)
    {
        config.NewConfig<Form, FormResponse>()
         .Map(dest => dest.Id, src => src.Id.Value.ToString())
         .Map(dest => dest.FirstSection, src => src.FirstSection.Value.ToString());

        config.NewConfig<FormSection, FormSectionResponse>()
         .Map(dest => dest.Id, src => src.Id.Value.ToString())
         .Map(dest => dest.DefaultNextSection, src => src.DefaultNextSection == null ? null : src.DefaultNextSection.Value.ToString())
         .Map(dest => dest.CanRepeat, src => src.Repeatable.CanRepeat)
         .Map(dest => dest.NumberOfReapeats, src => src.Repeatable.NumberOfReapeats);

        config.NewConfig<FormField, FormFieldResponse>()
         .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<FieldChoiceSettings, FormFieldChoiceSettingsResponse>()
            .Map(dest => dest.NextSection, src => src.NextSection == null ? null : src.NextSection.Value.ToString());

        config.NewConfig<FieldOptionObject, FieldOptionsValueResponse>()
           .Map(dest => dest.NextSection, src => src.NextSection == null ? null : src.NextSection.Value.ToString());

        config.NewConfig<Form, AllFormsResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Name, src => src.Name);
    }

}
