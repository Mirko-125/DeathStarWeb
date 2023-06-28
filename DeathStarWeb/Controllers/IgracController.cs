using Microsoft.AspNetCore.Mvc;
using DeathStar_new;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgracController : ControllerBase
    {
        [HttpGet("PreuzmiIgrace")]
        public IActionResult GetIgraci()
        {
            var result = DTOManager.vratiSveIgrace();

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpPost("DodajIgraca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DodajIgraca([FromBody] IgracPregled i)
        {
            var data = await DTOManager.dodajIgracaAsync(i);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodat igrač. Username: {i.username}");
        }

        [HttpPost("DodajPosadu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajPosadu(string username)
        {
            var data = await DTOManager.dodajPosaduIgracuAsync(username);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno alocirana posada Igracu id: {data.Data}");
        }

        [HttpPut("AzurirajIgraca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AzurirajIgraca([FromBody] IgracPregled i)
        {
            var data = await DTOManager.dodajIgracaAsync(i);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            if (data.Data == null)
            {
                return BadRequest("Igrač nije validan.");
            }

            return Ok($"Uspešno ažuriran igrač. Username: {i.username}");
        }

        



        [HttpDelete("ObrisiIgraca/{naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObrisiIgraca(string naziv)
        {
            var data = await DTOManager.obrisiIgracaAsync(naziv);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisan igrač. ID: {naziv}");
        }

    }
}
