using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public record DeleteSubscriptionCommand(string Id) : IRequest<ErrorOr<Subscription>>;
