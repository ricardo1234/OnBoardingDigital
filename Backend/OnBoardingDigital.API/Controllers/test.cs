using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingDigital.Infrastructure.EF;

namespace OnBoardingDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class test : ControllerBase
    {
        private OnBoardingDigitalDbContext _context;

        public test(OnBoardingDigitalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetForms()
        {
            return Ok(_context.Forms?.ToList());
        }
    }
}
