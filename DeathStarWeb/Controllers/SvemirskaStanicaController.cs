using Microsoft.AspNetCore.Mvc;
using DeathStar_new;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SvemirskaStanicaController : ControllerBase
    {
        [HttpGet("PreuzmiStanice")]
        public IActionResult GetStanice(int idPlanete, string tip)
        {
            var result = DTOManager.vratiSveStanicePlanete(idPlanete, tip);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpPost("DodajVojnuStanicu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostVojnuStanicu([FromBody] VojnaStanicaPregled vsp, int idp)
        {
            var data = await DTOManager.dodajVojnuStanicu(vsp, idp);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodata stanica. Naziv: {vsp.naziv}");
        }

        [HttpPost("DodajCivilnuStanicu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostCivilnuStanicu([FromBody] CivilnaSvemirskaStanicaPregled csp, int idp)
        {
            var data = await DTOManager.dodajCivilnuStanicu(csp, idp);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodata stanica. Naziv: {csp.naziv}");
        }

        [HttpPut("AzurirajVojnuSvemirskuStanicu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajVojnuSvemirskuStanicu([FromBody] VojnaStanicaPregled v)
        {
            var data = await DTOManager.azurirajVojnuStanicu(v);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno ažurirana stanica.");
        }
        [HttpPut("AzurirajCivilnuSvemirskuStanicu")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajCivilnuSvemirskuStanicu([FromBody] CivilnaSvemirskaStanicaPregled c)
        {
            var data = await DTOManager.azurirajCivilnuStanicu(c);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno ažurirana stanica.");
        }
        [HttpDelete("ObrisiStanicu/{idStanice}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteStanica(int idStanice)
        {
            var data = await DTOManager.obrisiStanicu(idStanice);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Obrisana stanica");
        }


    }


}