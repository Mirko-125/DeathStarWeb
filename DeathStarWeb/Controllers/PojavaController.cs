using DeathStar_new;
using Microsoft.AspNetCore.Mvc;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PojavaController : ControllerBase
    {
        [HttpGet("PreuzmiPojave/{idP}")]
        public IActionResult GetPojave(int idP)
        {
            var result = DTOManager.vratiSvePojavePlanete(idP);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("DodajPojavu/{idP}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPojava([FromBody] PojavaPregled p, int idP)
        {
            var data = await DTOManager.dodajPojavuAsync(p, idP);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodata pojava {p.naziv}");
        }

        [HttpPut("AzurirajPojavu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajPojavu([FromBody] PojavaPregled p)
        {
            var data = await DTOManager.azurirajPojavuAsync(p);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            if (data.Data == null)
            {
                return BadRequest("Pojava nije validna.");
            }

            return Ok($"Uspešno ažurirana pojava. Naziv: {p.naziv}");
        }

        [HttpDelete("ObrisiPojavu/{naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePojava(string naziv)
        {
            var data = await DTOManager.obrisiPojavuAsync(naziv);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisana pojava {naziv}");
        }
    }
}
