using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public record GetSubscriptionQuery(string Id) : IRequest<ErrorOr<Subscription>>;
