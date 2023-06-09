using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public record GetAllSubscriptionQuery(string Email) : IRequest<ErrorOr<List<Subscription>>>;
