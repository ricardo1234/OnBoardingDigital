using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.SubscriptionAggregate.Entities;

namespace OnBoardingDigital.API.Application.Queries.Subscriptions;

public record GetSubscriptionAnswerFileQuery(string IdSubs, string IdAnswer) : IRequest<ErrorOr<SubscriptionAnswer>>;
