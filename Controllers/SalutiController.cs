using Microsoft.AspNetCore.Mvc;

namespace AEP_WebApi.Controllers
{
    [ApiController]
    [Route("api/saluti")]
    public class SalutiController : ControllerBase
    {
        [HttpGet]
        public IActionResult getSaluti()
        {
            return Ok("Buongiorno, sono il primo web service creato in .net core 3");
        }
        [HttpGet("{nome}")]
        public IActionResult getSalutiNome(string nome)
        {
            return Ok(string.Format("Buongiorno {0}, sono il primo web service creato in .net core 3", nome));
        }
    }
}