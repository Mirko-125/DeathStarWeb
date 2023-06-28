using Microsoft.AspNetCore.Mvc;
using DeathStar_new;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrodController : ControllerBase
    {
        [HttpGet("PreuzmiBrodove")]
        public IActionResult GetStanice(int idPlanete, string tip)
        {
            var result = DTOManager.vratiSveBrodovePlanete(idPlanete, tip);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpPost("DodajBorbeniBrod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostBorbeniBrod([FromBody] BorbeniBrodPregled bbp, int idplanete, int idposade)
        {
            var data = await DTOManager.dodajBorbeniBrod(bbp, idplanete, idposade);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodat brod. Naziv: {bbp.naziv}");
        }

        [HttpPost("DodajTransportniBrod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostTransportniBrod([FromBody] TransportniBrodPregled tbp, int idplanete, int idposade)
        {
            var data = await DTOManager.dodajTransportniBrod(tbp, idplanete, idposade);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodat brod. Naziv: {tbp.naziv}");
        }

        [HttpDelete("ObrisiBrod/{idBroda}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStanica(int idBroda)
        {
            var data = await DTOManager.obrisiBrodAsync(idBroda);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Obrisan brod");
        }
    }


}