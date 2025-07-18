using Guidici.Business.Abstraction;
using Guidici.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Guidici.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GuidiceController : ControllerBase
{
    private readonly IBusiness _business;
    private readonly ILogger<GuidiceController> _logger;

    public GuidiceController(IBusiness business, ILogger<GuidiceController> logger)
    {
        _business = business;
        _logger = logger;
    }

    [HttpGet(Name = "GetGuidice")]
    public async Task<ActionResult<PersonaDto?>> GetGuidice(int id)
    {
        var persona= await _business.GetGuidice(id);

        if (persona == null)
            return NotFound();

        return new JsonResult(persona);
       
    }

    [HttpPost(Name = "AddGuidice")]
    public async Task<ActionResult> CreateGuidice(PersonaDto personaDto)
    {
        await _business.CreateGuidiceAsync(personaDto);
        return Ok("creato guidice");
    }

    [HttpPost(Name = "CreateVotazione")]
    public async Task<ActionResult> CreateVotazione(VotazioneDto votazioneDto)
    {
        await _business.CreateVotazione(votazioneDto);
        return Ok("creata votazione");
    }
}
