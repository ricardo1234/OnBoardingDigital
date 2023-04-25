using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnBoardingDigital.API.Application.Queries.GetForm;
using OnBoardingDigital.Contracts.Form;
using OnBoardingDigital.Domain.FormAggregate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnBoardingDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public FormController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET: api/<FormController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var getResult = await _mediator.Send(new GetAllFormQuery());

            return Ok(_mapper.Map<List<AllFormsResponse>>(getResult.Value));
        }

        // GET api/<FormController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetFormQuery(Guid.Parse(id));

            var getResult = await _mediator.Send(query);

            if (!getResult.IsError)
                return Ok(_mapper.Map<FormReponse>(getResult.Value));

            return NotFound(getResult.FirstError.Description);
        }
    }
}
