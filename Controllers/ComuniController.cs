using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AEP_WebApi.Models;
using AEP_WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AEP_WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/comuni")]
    public class ComuniController : Controller
    {
        private IComuniRepository comuniRepository;
        public ComuniController(IComuniRepository comuniRepository)
        {
            this.comuniRepository = comuniRepository;
        }

        /*[HttpGet("carica/comuni")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type= typeof(IEnumerable<ComuniDto>))]
        public async Task<IActionResult> GetComuni()
        {
            var ComuniDto = new List<ComuniDto>();
            var comuni = await this.comuniRepository.GetComuni();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (comuni.Count == 0)
            {
                return NotFound(string.Format("Non è stato trovato alcun comune."));
            }
            foreach(var comune in comuni)
            {
                ComuniDto.Add(new ComuniDto
                {
                    IdComune = comune.IdComune,
                    Comune = comune.Comune,
                    Cap = comune.Cap,
                    Provincia = comune.Provincia,
                    Regione = comune.Regione
                });
            }
            return Ok(ComuniDto);
        }*/

        [HttpGet("cerca/comune/{comune}", Name="GetComune")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type= typeof(IEnumerable<ComuniDto>))]
        public async Task<IActionResult> GetComune(string comune)
        {
            if (! await this.comuniRepository.EsisteComune(comune))
            {
                return NotFound(string.Format("Non è stato trovato nessun comune: {0}", comune));
            }

            var ricerca = await this.comuniRepository.GetComune(comune);
            var ComuniDto = new ComuniDto
            {
                IdComune = ricerca.IdComune,
                Comune = ricerca.Comune,
                Cap = ricerca.Cap,
                Provincia = ricerca.Provincia,
                Regione = ricerca.Regione
            };
            return Ok(ComuniDto);
        }

        [HttpGet("cerca/cap/{cap}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type= typeof(IEnumerable<ComuniDto>))]
        public async Task<IActionResult> GetCap(string cap)
        {
            if (! await this.comuniRepository.EsisteCap(cap))
            {
                return NotFound(string.Format("Non è stato trovato nessun comune con il cap: {0}", cap));
            }

            var ComuniDto = new List<ComuniDto>();
            var comuni = await this.comuniRepository.GetCap(cap);

            foreach(var comune in comuni)
            {
                ComuniDto.Add(new ComuniDto
                {
                    IdComune = comune.IdComune,
                    Comune = comune.Comune,
                    Cap = comune.Cap,
                    Provincia = comune.Provincia,
                    Regione = comune.Regione
                });
            }
            return Ok(ComuniDto);
        }

        [HttpPost("nuovo")]
        [ProducesResponseType(201, Type= typeof(Comuni))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult SalvaComune([FromBody] Comuni comune)
        {
            if (comune == null)
            {
                return BadRequest(ModelState);
            }
            var isPresent = comuniRepository.GetComune(comune.Comune);
            if (isPresent != null)
            {
                ModelState.AddModelError("", $"Non è possibile salvare il comune {comune.Comune} in quanto è già presente in anagrafica !");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                string ErrVal = "";
                foreach (var ModelState in ModelState.Values)
                {
                    foreach (var modelError in ModelState.Errors)
                    {
                        ErrVal += modelError.ErrorMessage + "|";
                    }
                }
                return BadRequest(ErrVal);
            }
            if (!comuniRepository.NuovoComune(comune))
            {
                ModelState.AddModelError("", $"Non è possibile salvare il comune {comune.Comune} in quanto sono stati trovati errori !");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetComune", new {comune = comune.Comune}, comune);
            //return Ok(new InfoMessage(DateTime.Today, $"Comune {comune.Comune} aggiunto con successo !"));
        }

        [HttpPost("modifica")]
        [ProducesResponseType(201, Type= typeof(InfoMessage))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult AggiornaComune([FromBody] Comuni comune)
        {
            if (comune == null)
            {
                return BadRequest(ModelState);
            }
            var isPresent = comuniRepository.GetComune(comune.Comune);
            if (isPresent == null)
            {
                ModelState.AddModelError("", $"Non è possibile aggiornare il comune {comune.Comune} in quanto non è presente in anagrafica !");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!comuniRepository.ModificaComune(comune))
            {
                ModelState.AddModelError("", $"Non è possibile aggiornare il comune {comune.Comune} in quanto sono stati trovati errori !");
                return StatusCode(500, ModelState);
            }
            return Ok(new InfoMessage(DateTime.Today, $"Comune {comune.Comune} aggiornato con successo !"));
        }
    }
}