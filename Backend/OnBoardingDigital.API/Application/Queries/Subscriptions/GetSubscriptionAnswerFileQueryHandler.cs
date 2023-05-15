using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate.Entities;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public sealed class GetSubscriptionAnswerFileQueryHandler : IRequestHandler<GetSubscriptionAnswerFileQuery, ErrorOr<SubscriptionAnswer>>
{
    private readonly ISubscriptionRepository subscriptionRepository;

    public GetSubscriptionAnswerFileQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        this.subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<SubscriptionAnswer>> Handle(GetSubscriptionAnswerFileQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.IdSubs, out _))
            return Error.Validation("Subscription.InvalidId", "The id provided is not a GUID.");
        
        if (!Guid.TryParse(request.IdAnswer, out _))
            return Error.Validation("Subscription.InvalidId", "The id provided is not a GUID.");

        var subscriptionId = SubscriptionId.CreateFromString(request.IdSubs);

        var subs = await subscriptionRepository.GetByIdAsync(subscriptionId);
        if (subs is null)
            return Error.NotFound("Subscription.NotFound", "Subscritpion was not found.");

        var answerId = SubscriptionAnswerId.CreateFromString(request.IdAnswer);

        var ans = subs.Answers.FirstOrDefault(a => a.Id.Equals(answerId));
        if (ans is null)
            return Error.NotFound("Subscription.NotFound", "Subscritpion answer was not found.");

        if(!ans.Value.FieldType.Equals(FieldType.File))
            return Error.Validation("Subscription.Invalid", "Subscritpion answer is not of file type.");

        if(ans.Value.FileBytes is null)
            return Error.NotFound("Subscription.NotFound", "File is empty.");

        return ans;
    }
}
