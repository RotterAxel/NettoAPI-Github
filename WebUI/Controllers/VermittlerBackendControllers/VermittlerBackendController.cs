using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.Profil.Commands;
using Application.VermittlerBackend.Profil.Queries.GetDokument;
using Application.VermittlerBackend.Profil.Queries.GetVermittlerGesellschaften;
using Application.VermittlerBackend.Profil.Queries.GetVermittlerPolicy;
using Application.VermittlerBackend.Profil.Queries.GetVermittlerProfil;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.VermittlerBackendControllers
{
    public class VermittlerBackendController : ApiController
    {
        [HttpGet("Policy")]
        [Authorize]
        [Produces("application/json")]
        public async Task<ActionResult<VermittlerPolicyDto>> GetVermittlerPolicy()
        {
            try
            {
                return Ok(await Mediator.Send(new GetVermittlerPolicyQuery()));
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        
        [HttpGet("{dokumentId}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RegistrierungsDokumentDto>> GetDokument(int dokumentId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetDokumentQuery()
                {
                    DokumentId = dokumentId
                }));
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
        
        [HttpGet("Profil")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VermittlerProfilDto>> GetProfil()
        {
            try
            {
                return Ok(await Mediator.Send(new GetVermittlerProfilQuery()));
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
        
        [HttpPut("Profil")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> RegisterOrUpdateVermittler( 
            [FromBody] UpdateVermittlerProfilCommand command)
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
        
        [HttpGet("VermittlerGesellschaften")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<VermittlerGesellschaftDto>>> GetVermittlerGesellschaften()
        {
            try
            {
                return Ok(await Mediator.Send(new GetVermittlerGesellschaftenQuery()));
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}