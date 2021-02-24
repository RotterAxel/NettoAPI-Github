using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.VermittlerRegistrierung.Commands.CreateOrUpdateDokument;
using Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegisterOrUpdateVermittler;
using Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegistrierungBeenden;
using Application.VermittlerBackend.VermittlerRegistrierung.Queries.GetEinladecodeValidität;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.VermittlerBackendControllers
{
    public class VermittlerRegistrierungController : ApiController
    {
        [HttpGet("EinladecodeValidierung/{einladecode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetEinladecodeValidität(string einladecode)
        {
            if (einladecode == "\"\"" || einladecode == "null")
                return BadRequest("Einladecode cannot be null or empty.");

            try
            {
                return Ok(await Mediator.Send(new GetEinladecodeValiditätQuery()
                {
                    Code = einladecode
                }));
            }
            catch (NotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (InternalServerException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (FormatException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Einladecode has to be a valid Base64 String");
            }
        }
        
        [HttpPost("{id}/Dokument")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateOrUpdateDokumentFürVermittler(int id, 
            [FromBody] CreateOrUpdateDokumentFürVermittlerCommand command)
        {
            if (id != command.VermittlerId)
                return BadRequest();

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> RegisterOrUpdateVermittler( 
            [FromBody] RegisterOrUpdateVermittlerCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPost("RegistrierungBeenden")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> RegistrierungBeenden()
        {
            try
            {
                return Ok(await Mediator.Send(new RegistrierungBeendenCommand()));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}