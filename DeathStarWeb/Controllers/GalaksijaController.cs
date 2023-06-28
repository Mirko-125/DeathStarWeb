using Microsoft.AspNetCore.Mvc;
using DeathStar_new;

namespace DeathStarWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalaksijaController : ControllerBase
    {
        [HttpGet("PreuzmiGalaksije")]
        public IActionResult GetGalaksije()
        {
            var result = DTOManager.vratiSveGalaksije();

            if (result.IsError)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpPost("DodajGalaksiju")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> dodajGalaksiju([FromBody] GalaksijaPregled g)
        {
            var data = await DTOManager.dodajGalaksijuAsync(g);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno dodata galaksija. Naziv: {g.naziv}");
        }

        [HttpPut("AzurirajGalaksiju")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> azurirajGalaksiju([FromBody] GalaksijaPregled g)
        {
            var data = await DTOManager.dodajGalaksijuAsync(g);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            if (data.Data == null)
            {
                return BadRequest("Galaksija nije validna.");
            }

            return Ok($"Uspešno ažurirana galaksija. Naziv: {g.naziv}");
        }

        [HttpDelete("ObrisiGalaksiju/{naziv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGalaksija(string naziv)
        {
            var data = await DTOManager.obrisiGalaksijuAsync(naziv);

            if (data.IsError)
            {
                return BadRequest(data.Error);
            }

            return Ok($"Uspešno obrisana galaksija. ID: {naziv}");
        }


    }

    
}