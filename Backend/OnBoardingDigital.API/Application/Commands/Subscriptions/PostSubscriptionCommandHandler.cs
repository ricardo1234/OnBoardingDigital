using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public class PostSubscriptionCommandHandler : IRequestHandler<PostSubscriptionCommand, ErrorOr<bool>>
{
    private readonly ISubscriptionRepository subscriptionRepository;
    private readonly IFormRepository formRepository;

    public PostSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IFormRepository formRepository)
    {
        this.subscriptionRepository = subscriptionRepository;
        this.formRepository = formRepository;
    }

    public async Task<ErrorOr<bool>> Handle(PostSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var formId = FormId.CreateFromString(request.Subscription.FormId);

        var form = await formRepository.GetByIdAsync(formId);

        if(form is null)
            return Error.NotFound("Form.NotFound", "Form was not found.");


        return true;
    }
}
