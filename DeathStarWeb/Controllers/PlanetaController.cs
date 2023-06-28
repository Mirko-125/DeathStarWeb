using DeathStar_new;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetaController : ControllerBase
    {
        [HttpGet("PreuzmiPlanete/{nazivG}")]
        public IActionResult GetPlanete(string nazivG)
        {
            var result = DTOManager.vratiSvePlaneteGalaksije(nazivG);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        [HttpPost("DodajPlanetu/{nazivG}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPlaneta([FromBody] PlanetaPregled p, string nazivG)
        {
            var result = await DTOManager.dodajPlanetuAsync(p, nazivG);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            return Ok($"Uspešno dodata planeta. ID: {result.Data.idPlanete}");
        }

        [HttpPut("DodajPlanetuIgracu/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajPlanetuIgracu(string username, int idP)
        {
            var data = await DTOManager.dodajPlanetuIgracuAsync(username, idP);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            if (data.Data == null)
            {
                return BadRequest("Igrač nije validan.");
            }

            return Ok($"Uspešno dodata planeta igracu");
        }

        [HttpPut("AzurirajPlanetu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajPlaneta([FromBody] PlanetaBasic p)
        {
            var data = await DTOManager.azurirajPlanetuAsync(p);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno ažurirana planeta. Naziv: {data.Data}");
        }

        [HttpDelete("ObrisiPlanetu/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePlaneta(int id)
        {
            var data = await DTOManager.obrisiPlanetuAsync(id);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisana planeta. ID: {id}");
        }

        [HttpPost("OsvojiPlanetu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> osvojiPlanetu([FromQuery, Required] int idPlanete,[FromQuery, Required] int posadaId)
        {
            var result = await DTOManager.osvojiPlanetuAsync(idPlanete, posadaId);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }

            return Ok($"Uspešno osvojena planeta.");
        }

        [HttpPost("DodajGradPlaneti/{idPlanete}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajGradPlaneti(string nazivGrada, int idPlanete)
        {
            var data = await DTOManager.dodajGradPlanetiAsync(nazivGrada, idPlanete);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodat grad");
        }


    }
}
