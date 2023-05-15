using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public sealed class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository subscriptionRepository;

    public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        this.subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out _))
            return Error.Validation("Subscription.InvalidId", "The id provided is not a GUID.");

        var subscriptionId = SubscriptionId.CreateFromString(request.Id);

        var subs = await subscriptionRepository.GetByIdAsync(subscriptionId);
        if (subs is null)
            return Error.NotFound("Subscription.NotFound", "Subscritpion was not found.");

        return subs;
    }
}