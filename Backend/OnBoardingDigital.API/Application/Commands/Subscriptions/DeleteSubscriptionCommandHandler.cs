using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.Common;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository subscriptionRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IFormRepository formRepository, IUnitOfWork unitOfWork)
    {
        this.subscriptionRepository = subscriptionRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.Id, out _))
            return Error.Validation("Subscription.InvalidId", "The id provided is not a GUID.");

        var subscriptionId = SubscriptionId.CreateFromString(request.Id);

        var subs = await subscriptionRepository.GetByIdAsync(subscriptionId);
        if(subs is null)
            return Error.NotFound("Subscription.NotFound", "Subscritpion was not found.");

        subscriptionRepository.Remove(subs);

        await unitOfWork.CommitAsync();

        return subs;
    }
}
