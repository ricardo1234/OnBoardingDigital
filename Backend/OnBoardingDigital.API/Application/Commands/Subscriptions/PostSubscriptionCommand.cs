using ErrorOr;
using MediatR;
using OnBoardingDigital.Contracts.Subscription;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public record PostSubscriptionCommand(SubscriptionRequest Subscription, List<FileRequest> Files) : IRequest<ErrorOr<bool>>;

public record FileRequest(string Id, string Name, string Type, byte[] file);
