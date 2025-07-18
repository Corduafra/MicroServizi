using Microsoft.AspNetCore.Mvc;
using Registrazioni.Business.Abstraction;
using Registrazioni.Shared;

namespace Registrazione.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CaneController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<CaneController> _logger;

        public CaneController(IBusiness business, ILogger<CaneController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name ="CreateCane")]
        public async Task<ActionResult> CreateCane(CaneDto caneDto)
        {
            await _business.CreateCaneAsync(caneDto);
            return Ok("creato!");
        }

        [HttpGet(Name = "GetCane")]
        public async Task<ActionResult<CaneDto?>> GetCane(int id)
        {
            var cane = await _business.GetCaneDtoAsync(id);
            if (cane == null)
            {
                return NotFound();
            }
            return new JsonResult(cane);
        }

    }
}
