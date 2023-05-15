using Mapster;
using OnBoardingDigital.Contracts.Subscription;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.Entities;

namespace OnBoardingDigital.API.Mapping;

public class SubscriptionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        SubscriptionResponse(config);
    }

    private void SubscriptionResponse(TypeAdapterConfig config)
    {
        config.NewConfig<Subscription, AllSubscriptionResponse>()
             .Map(dest => dest.Id, src => src.Id.Value.ToString())
             .Map(dest => dest.FormId, src => src.FormId.Value.ToString());
        
        config.NewConfig<Subscription, SubscriptionResponse>()
             .Map(dest => dest.Id, src => src.Id.Value.ToString())
             .Map(dest => dest.FormId, src => src.FormId.Value.ToString());

        config.NewConfig<SubscriptionAnswer, SubscriptionAnswerResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.FieldId, src => src.FieldId.Value.ToString())
            .Map(dest => dest.NumberValue, src => src.Value.NumberValue)
            .Map(dest => dest.TextValue, src => src.Value.TextValue)
            .Map(dest => dest.OptionsValue, src => src.Value.OptionsValue)
            .Map(dest => dest.ChoiceValue, src => src.Value.ChoiceValue)
            .Map(dest => dest.FileName, src => src.Value.FileName);
    }
}
