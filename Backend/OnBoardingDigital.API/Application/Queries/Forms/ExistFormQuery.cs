using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate;

namespace OnBoardingDigital.API.Application.Queries.Forms;

public record ExistFormQuery(Guid id) : IRequest<ErrorOr<bool>>;
