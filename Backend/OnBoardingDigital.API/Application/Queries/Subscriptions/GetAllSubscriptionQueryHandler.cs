using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public sealed class GetAllSubscriptionQueryHandler : IRequestHandler<GetAllSubscriptionQuery, ErrorOr<List<Subscription>>>
{
    private readonly ISubscriptionRepository subscriptionRepository;

    public GetAllSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        this.subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<List<Subscription>>> Handle(GetAllSubscriptionQuery request, CancellationToken cancellationToken)
    {
        return await subscriptionRepository.GetByEmailAsync(request.Email);
    }
}