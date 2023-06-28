using DeathStar_new;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrirodniSatelitController : ControllerBase
    {
        [HttpGet("PreuzmiPrirodneSatelite/{idP}")]
        public IActionResult GetPrirodniSateliti(int idP)
        {
            var result = DTOManager.vratiSvePrirodneSatelite(idP);

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }
        [HttpPost("PostaviPrirodniSatelit/{idP}")]
        public async Task<IActionResult> PostPrirodniSatelit(PrirodniSatelitPregled psp, int idp)
        {
            var result = await DTOManager.dodajPrirodniSatelit(psp, idp);
            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }
        
        [HttpPut("AzurirajSatelit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajPrirodniSatelit([FromBody] PrirodniSatelitPregled psp)
        {
            var data = await DTOManager.azurirajPrirodniSatelit(psp);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            if (data.Data == null)
            {
                return BadRequest("Satelit je valja.");
            }

            return Ok($"Uspešno ažuriran satelit. Naziv: {psp.naziv}");
        }

        [HttpDelete("ObrisiPrirodniSatelit")]
        public async Task<IActionResult> DeletePrirodniSatelit(string naziv)
        {
            var data = await DTOManager.obrisiPrirodniSatelitAsync(naziv);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisan satelit. Naziv: {naziv}");
        }

    }
}
