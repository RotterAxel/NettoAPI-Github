using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Stammdaten.Queries.GetBerufe;
using Application.Stammdaten.Queries.GetDokumentArten;
using Application.Stammdaten.Queries.GetLänder;
using Application.Stammdaten.Queries.GetTitel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Stammdaten
{
    public class StammdatenController : ApiController
    {
        [HttpGet("DokumentArt")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public async Task<ActionResult<IList<DokumentArtÜbersichtDto>>> GetDokumentArten(
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return StatusCode(StatusCodes.Status415UnsupportedMediaType);
            }

            return Ok(await Mediator.Send(new GetDokumentArtenQuery()));
        }
        
        [HttpGet("Laender")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public async Task<ActionResult<IList<LandDto>>> GetLänder(
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return StatusCode(StatusCodes.Status415UnsupportedMediaType);
            }
            
            return Ok(await Mediator.Send(new GetLänderQuery()));
        }
        
        [HttpGet("Beruf")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public async Task<ActionResult<IList<BerufDto>>> GetBerufe(
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return StatusCode(StatusCodes.Status415UnsupportedMediaType);
            }

            return Ok(await Mediator.Send(new GetBerufeQuery()));
        }
        
        [HttpGet("Titel")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public async Task<ActionResult<IList<TitelDto>>> GetTitel(
            [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType,
                out MediaTypeHeaderValue parsedMediaType))
            {
                return StatusCode(StatusCodes.Status415UnsupportedMediaType);
            }

            return Ok(await Mediator.Send(new GetTitelQuery()));
        }
    }
}