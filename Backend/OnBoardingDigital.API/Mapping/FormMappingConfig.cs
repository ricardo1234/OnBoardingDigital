using Mapster;
using OnBoardingDigital.Contracts.Form;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.Entities;

namespace OnBoardingDigital.API.Mapping;

public class FormMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        FormResponse(config);
    }


    private void FormResponse(TypeAdapterConfig config)
    {
        config.NewConfig<Form, FormReponse>()
         .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<FormSection, FormSectionResponse>()
         .Map(dest => dest.Id, src => src.Id.Value.ToString())
         .Map(dest => dest.CanRepeat, src => src.Repeatable.CanRepeat)
         .Map(dest => dest.NumberOfReapeats, src => src.Repeatable.NumberOfReapeats);

        config.NewConfig<FormField, FormFieldResponse>()
         .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<Form, AllFormsResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Name, src => src.Name);
    }

}
