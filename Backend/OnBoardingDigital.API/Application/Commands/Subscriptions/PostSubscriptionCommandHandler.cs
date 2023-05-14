using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;

namespace OnBoardingDigital.API.Application.Commands.Subscriptions;

public class PostSubscriptionCommandHandler : IRequestHandler<PostSubscriptionCommand, ErrorOr<bool>>
{
    private readonly IFormRepository formRepository;

    public PostSubscriptionCommandHandler(IFormRepository formRepository)
    {
        this.formRepository = formRepository;
    }

    public async Task<ErrorOr<bool>> Handle(PostSubscriptionCommand request, CancellationToken cancellationToken)
    {

        return true;
    }
}
