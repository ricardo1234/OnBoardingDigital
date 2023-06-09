using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;
using OnBoardingDigital.Domain.SubscriptionAggregate;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions
{
    public class GetSubscriptionReportQueryHandler : IRequestHandler<GetSubscriptionReportQuery, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        public GetSubscriptionReportQueryHandler(ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
        }

        public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionReportQuery request, CancellationToken cancellationToken)
        {
           // XtraReport rep = DevExpress.XtraReports.UI..FromFile("./Aplication/Report/ExtratoDetalhadoMB.repx",true);

            return Error.Failure();
        }
    }
}
