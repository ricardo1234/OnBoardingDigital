using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.SubscriptionAggregate;
using OnBoardingDigital.Domain.SubscriptionAggregate.ValueObjects;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public record GetSubscriptionReportQuery(string Id) : IRequest<ErrorOr<Report>>;
