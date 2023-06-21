using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Components.RenderTree;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions
{
    public class GetSubscriptionReportQueryHandler : IRequestHandler<GetSubscriptionReportQuery, ErrorOr<Report>>
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IFormRepository formRepository;

        public GetSubscriptionReportQueryHandler(ISubscriptionRepository subscriptionRepository, IFormRepository formRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.formRepository = formRepository;
        }

        public async Task<ErrorOr<Report>> Handle(GetSubscriptionReportQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.Id, out _))
                return Error.Validation("Subscription.InvalidId", "The id provided is not a GUID.");

            var subscriptionId = SubscriptionId.CreateFromString(request.Id);

            var subs = await subscriptionRepository.GetByIdAsync(subscriptionId);
            if (subs is null)
                return Error.NotFound("Subscription.NotFound", "Subscritpion was not found.");

            var form = await formRepository.GetByIdAsync(subs.FormId);

            if (form is null)
                return Error.NotFound("Form.NotFound", "Form was not found.");

            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.InputEncoding = System.Text.Encoding.UTF8;
            using var PDf = renderer.RenderHtmlAsPdf(ReportService.GetTempalte(form, subs));

            return Report.Create(subscriptionId, PDf.BinaryData, $"{form.Name}_{subs.Email.Split('@')[0].Replace(".","")}_{subs.CreatedAtUtc:ddMMyyyy}");
        }
    }
}
