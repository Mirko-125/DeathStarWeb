using Microsoft.AspNetCore.Mvc;
using DeathStar_new;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SavezController : ControllerBase
    {
        [HttpGet("PreuzmiSaveze")]
        public IActionResult GetSavezi()
        {
            var result = DTOManager.vratiSveSaveze();

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpPost("DodajSavez")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajSavez(string nazivS)
        {
            var data = await DTOManager.dodajSavezAsync(nazivS);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodat savez. Naziv: {nazivS}");
        }

        [HttpPost("PridruziSavez/{nazivS1}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> pridruziSavez(string nazivS1, string nazivS2)
        {
            var data = await DTOManager.pridruziSavezeAsync(nazivS1, nazivS2);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno pridruzeni svezi");
        }

        [HttpPost("DodajClana/{nazivI}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajClana(string nazivS, string nazivI)
        {
            var data = await DTOManager.dodajClanaAsync(nazivS, nazivI);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno pridruzeni svezi");
        }

        [HttpPost("AlocirajPosadu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajPosadu(string nazivS)
        {
            var data = await DTOManager.dodajPosaduSavezuAsync(nazivS);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno alocirana posada, ID: {data.Data}");
        }

        [HttpDelete("ObrisiSavez/{naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSavez(string naziv)
        {
            var data = await DTOManager.obrisiSavezAsync(naziv);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisan savez. Naziv: {naziv}");
        }
    }
}
