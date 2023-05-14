using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnBoardingDigital.API.Application.Commands.Subscriptions;
using OnBoardingDigital.API.Application.Queries.Forms;
using OnBoardingDigital.Contracts.Subscription;
using OnBoardingDigital.Domain.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnBoardingDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ISubscriptionRepository _repo;

        public SubscriptionController(ISender mediator, ISubscriptionRepository repo)
        {
            _mediator = mediator;
            _repo = repo;
        }

        // GET: api/<SubscriptionController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result);
        }

        //// GET api/<SubscriptionController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<SubscriptionController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] IFormFileCollection files)
        {
            SubscriptionRequest? data = null;
            
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

        //// PUT api/<SubscriptionController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SubscriptionController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
