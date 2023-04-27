using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;

namespace OnBoardingDigital.API.Application.Queries.Forms;

public class GetFormQueryHandler : IRequestHandler<GetFormQuery, ErrorOr<Form>>
{
    private readonly IFormRepository formRepository;

    public GetFormQueryHandler(IFormRepository formRepository)
    {
        this.formRepository = formRepository;
    }

    public async Task<ErrorOr<Form>> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        var formId = FormId.Create(request.id);
        var form = await formRepository.GetByIdAsync(formId);

        if (form is null)
            return Error.NotFound("Form.NotFound", "Form was not found.");

        return form;
    }
}
