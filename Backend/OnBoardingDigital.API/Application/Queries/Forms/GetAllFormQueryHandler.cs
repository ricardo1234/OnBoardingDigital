using ErrorOr;
using MediatR;
using OnBoardingDigital.Domain.FormAggregate;
using OnBoardingDigital.Domain.Repositories;

namespace OnBoardingDigital.API.Application.Queries.Forms;

public class GetAllFormQueryHandler : IRequestHandler<GetAllFormQuery, ErrorOr<List<Form>>>
{
    private readonly IFormRepository formRepository;

    public GetAllFormQueryHandler(IFormRepository formRepository)
    {
        this.formRepository = formRepository;
    }

    public async Task<ErrorOr<List<Form>>> Handle(GetAllFormQuery request, CancellationToken cancellationToken)
    {
        var form = await formRepository.GetAllAsync();

        return form;
    }
}
