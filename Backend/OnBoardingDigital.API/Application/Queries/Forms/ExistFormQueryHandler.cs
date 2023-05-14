using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate.ValueObjects;
using OnBoardingDigital.Domain.Repositories;

namespace OnBoardingDigital.API.Application.Queries.Forms;

public class ExistFormQueryHandler : IRequestHandler<ExistFormQuery, ErrorOr<bool>>
{
    private readonly IFormRepository formRepository;

    public ExistFormQueryHandler(IFormRepository formRepository)
    {
        this.formRepository = formRepository;
    }

    public async Task<ErrorOr<bool>> Handle(ExistFormQuery request, CancellationToken cancellationToken)
    {
        var formId = FormId.Create(request.id);
        var form = await formRepository.FormExistsAsync(formId);

        if (!form)
            return Error.NotFound("Form.NotFound", "Form was not found.");

        return form;
    }
}
