using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnBoardingDigital.API.Application.Commands.Subscriptions;
using OnBoardingDigital.API.Application.Queries.Subscriptions;
using OnBoardingDigital.Contracts.Subscription;
using MimeTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnBoardingDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public SubscriptionController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        // GET: api/<SubscriptionController>
        [HttpGet("{idSubs}/GetFile/{idAnswer}")]
        public async Task<IActionResult> GetByEmail(string idSubs, string idAnswer)
        {
            var result = await _mediator.Send(new GetSubscriptionAnswerFileQuery(idSubs, idAnswer));

            if (result.IsError)
                return result.FirstError.Type switch
                {
                    ErrorType.NotFound => NotFound(result.FirstError.Description),
                    ErrorType.Validation => BadRequest(result.FirstError.Description),
                    _ => Problem("Could not process the request.")
                };

            return new FileContentResult(result.Value.Value.FileBytes!, MimeTypeMap.GetMimeType(result.Value.Value.FileName));
        }

        // GET: api/<SubscriptionController>
        [HttpGet("ByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _mediator.Send(new GetAllSubscriptionQuery(email));

            if (!result.IsError) 
                return Ok(_mapper.Map<List<AllSubscriptionResponse>>(result.Value));

            return BadRequest(result.FirstError);
        }

        // GET api/<SubscriptionController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _mediator.Send(new GetSubscriptionQuery(id));

            if (!result.IsError)
                return Ok(_mapper.Map<SubscriptionResponse>(result.Value));

            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Validation => BadRequest(result.FirstError.Description),
                _ => Problem("Could not process the request.")
            };
        }

        // POST api/<SubscriptionController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] IFormFileCollection _)
        {
            SubscriptionRequest? data = null;
            IFormFileCollection files;

            var canParse = Request.Form.TryGetValue("subscription", out var subscription);
            
            if (canParse)
                data = JsonConvert.DeserializeObject<SubscriptionRequest>(subscription.ToString());

            if (data is null)
                return BadRequest("The subscrition information is required");

            files = HttpContext.Request.Form.Files;
            var filesList = new List<FileRequest>();
            foreach(var file in files)
            {
                if (file.Length > 0)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    filesList.Add(new FileRequest(file.Name, file.FileName, file.FileName.Split('.').Last(), fileBytes));
                }
            }

            var result = await _mediator.Send(new PostSubscriptionCommand(data, filesList));

            if (!result.IsError)
                return Ok(result.Value);

            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Failure => Problem(result.FirstError.Description),
                ErrorType.Validation => ValidationProblem(result.FirstError.Description),
                _ => Problem("Could not process the request.")
            };
        }

        // DELETE api/<SubscriptionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteSubscriptionCommand(id));

            if (!result.IsError)
                return NoContent();

            return result.FirstError.Type switch
            {
                ErrorType.NotFound => NotFound(result.FirstError.Description),
                ErrorType.Validation => BadRequest(result.FirstError.Description),
                _ => Problem("Could not process the request.")
            };
        }
    }
}
