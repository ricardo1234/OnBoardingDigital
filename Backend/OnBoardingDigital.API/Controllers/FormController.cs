using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnBoardingDigital.API.Application.Commands.Forms;
using OnBoardingDigital.API.Application.Queries.Forms;
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
            if (!Guid.TryParse(id, out Guid _id))
                return BadRequest("The field id is not a valid Guid.");
            
            var query = new GetFormQuery(_id);

            var getResult = await _mediator.Send(query);

            if (!getResult.IsError)
                return Ok(_mapper.Map<FormResponse>(getResult.Value));

            return NotFound(getResult.FirstError.Description);
        }
        // GET api/<FormController>/exists/5
        [HttpGet("Exists/{id}")]
        public async Task<IActionResult> Exists(string id)
        {
            if (!Guid.TryParse(id, out Guid _id))
                return BadRequest("The field id is not a valid Guid."); 

            var query = new ExistFormQuery(_id);

            var getResult = await _mediator.Send(query);

            if (!getResult.IsError)
                return Ok();

            return NotFound(getResult.FirstError.Description);
        }

        // POST api/<FormController>/randomadd
        [HttpPost("RandomAdd")]
        public async Task<IActionResult> AddSectionToForm()
        {
            var query = new ChangeFormCommand("b7a6c87c-553d-4a13-ab0d-b8d7f576e8c9");

            var getResult = await _mediator.Send(query);

            if (!getResult.IsError)
                return Ok();

            return NotFound(getResult.FirstError.Description);
        }

    }
}
