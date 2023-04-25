using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate;

namespace OnBoardingDigital.API.Application.Queries.GetForm;

public record GetFormQuery(Guid id) : IRequest<ErrorOr<Form>>;
