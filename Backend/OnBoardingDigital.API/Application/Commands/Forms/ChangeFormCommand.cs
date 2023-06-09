using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate;

namespace OnBoardingDigital.API.Application.Commands.Forms;

public record ChangeFormCommand(string Id) : IRequest<ErrorOr<Form>>;
