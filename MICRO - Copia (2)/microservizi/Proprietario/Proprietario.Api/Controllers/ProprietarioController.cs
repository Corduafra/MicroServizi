using Microsoft.AspNetCore.Mvc;
using Proprietario.Business.Abstraction;
using Proprietario.Shared;

namespace Proprietario.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProprietarioController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<ProprietarioController> _logger;

        public ProprietarioController(IBusiness business, ILogger<ProprietarioController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet(Name = "GetProprietario")]
        public async Task<ActionResult<SoggettoDto?>> GetProprietario(int id)
        {
            var soggetto = await _business.GetProprietario(id);
            if (soggetto == null)
            {
                return NotFound();
            }
            return new JsonResult(soggetto);
        }

        [HttpPost(Name = "CreateSoggetto")]
        public async Task<ActionResult> CreateSoggetto(SoggettoDto soggettoDto)
        {
            await _business.CreateSoggetto(soggettoDto);
            return Ok("Creato proprietario");
        }

        [HttpPost(Name = "AssociaCaneALProprietario")]
        public async Task<ActionResult> AssociaCaneALProprietario(CaneProprietarioDto caneProprietarioDto)
        {
 
            await _business.CaneProprietario(caneProprietarioDto);
            return Ok("Cane associato al proprietario");
        }

       
    }
}

