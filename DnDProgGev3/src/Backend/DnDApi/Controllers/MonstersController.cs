using DnD.Spells.Api.Contracts.DTO;
using DnDApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DnDApi.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class MonstersController : Controller {

        private readonly IMonsterService _monsterservice;

        public MonstersController(IMonsterService monsterservice) {
            _monsterservice = monsterservice;
        }


        [HttpGet]
        [Route("{id}")]
        [EnableRateLimiting("DnDApiFixed")]
        [ProducesResponseType<SpellResponseDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<ActionResult<MonsterResponseDTO>> GetMonsterAsync(string id) {
            var monster = await _monsterservice.GetMonsterAsync(id);
            if (monster == null) {
                return NotFound();
            }
            return Ok(monster);
        }




    }
}
