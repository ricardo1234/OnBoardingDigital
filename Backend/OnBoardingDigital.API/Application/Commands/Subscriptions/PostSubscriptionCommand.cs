using ErrorOr;
using MediatR;
using OnBoardingDigital.Contracts.Subscription;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public record PostSubscriptionCommand(SubscriptionRequest Subscription, List<FileRequest> Files) : IRequest<ErrorOr<Subscription>>;

public record FileRequest(string Id, string Name, string Type, byte[] file);
