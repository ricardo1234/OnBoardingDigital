using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate;

namespace OnBoardingDigital.API.Application.Queries.Forms;

public record GetAllFormQuery() : IRequest<ErrorOr<List<Form>>>;
