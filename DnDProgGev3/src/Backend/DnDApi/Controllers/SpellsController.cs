using DnD.Spells.Api.Contracts.DTO;
using DnDApi.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DnDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpellsController : ControllerBase
    {
        private readonly ISPellsService _spellsService;

        public SpellsController(ISPellsService spellsService)
        {
            _spellsService = spellsService;
        }



        [HttpGet]
        [Route("{id}")]
        [EnableRateLimiting("DnDApiFixed")]
        [ProducesResponseType<SpellResponseDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpellResponseDTO>> GetSpellAsync(string id){
          
            var spell = await _spellsService.GetSpellAsync(id);
            if (spell == null) {
                return NotFound();
            }
            return Ok(spell);
        }



    }
}
