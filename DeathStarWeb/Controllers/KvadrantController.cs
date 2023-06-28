using DeathStar_new;
using Microsoft.AspNetCore.Mvc;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KvadrantController : ControllerBase
    {
        [HttpGet("PreuzmiKvadrante/{naziv}")]
        public IActionResult GetKvadranti(string naziv)
        {
            var result = DTOManager.vratiSveKvadranteGalaksije(naziv);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("DodajKvadrant/{nazivG}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddKvadrant([FromBody] int precnik, string nazivG)
        {
            var data = await DTOManager.dodajKvadrantAsync(precnik, nazivG);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodan kvadrant. ID: {data.Data.redniBroj}");
        }

        [HttpPut("AzurirajKvadrant/{nazivG}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajKvadrant([FromBody] KvadrantPregled k, string nazivG)
        {
            var data = await DTOManager.azurirajKvadrantAsync(k);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            if (data.Data == null)
            {
                return BadRequest("Kvadrant nije validan.");
            }

            return Ok($"Uspešno ažuriran kvadrant. Naziv: {data.Data.redniBroj}");
        }

        [HttpDelete("ObrisiKvadrant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteKvadrant(int id)
        {
            var data = await DTOManager.obrisiKvadrantAsync(id);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisan kvadrant. ID: {id}");
        }
    }
}
