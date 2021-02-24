using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.CreateDokumentForVermittler;
using Application.InsuranceAdmin.Commands.DeleteDokumentFürVermittler;
using Application.InsuranceAdmin.Commands.UpdateBearbeitungsstatusFürDokument;
using Application.InsuranceAdmin.Commands.UpdateVermittler;
using Application.InsuranceAdmin.Query.GetDokumentFürVermittler;
using Application.InsuranceAdmin.Query.GetVermittler;
using Application.InsuranceAdmin.Query.GetVermittlerDetail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.AdminControllers
{
    public class AdminVermittlerController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IList<VermittlerÜbersichtDto>>> GetVermittler()
        {
            try
            {
                return Ok(await Mediator.Send(new GetVermittlerQuery()));
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        
        [HttpGet("{id}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VermittlerDetailansichtDto>> GetVermittlerDetail(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetVermittlerDetailQuery()
                {
                    VermittlerId = id
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

        [HttpGet("{id}/Dokument/{dokumentId}")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VermittlerDokumentDto>> GetDokumentFürVermittler(int id, int dokumentId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetDokumentFürVermittlerQuery()
                {
                    Id = id,
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
        
        [HttpPost("{id}/Dokument")]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateDokumentFürVermittler(int id, 
            [FromBody] CreateDokumentFürVermittlerCommand command)
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
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        
        [HttpPut("{id}/Dokument/{dokumentId}")]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateBearbeitungsstatusVonDokumentFürVermittler(int id, int dokumentId,
            [FromBody] UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand command)
        {
            if (id != command.VermittlerId)
                return BadRequest();

            if (dokumentId != command.DokumentId)
                return BadRequest();
            
            try
            {
                await Mediator.Send(command);
                return NoContent();
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
        
        [HttpDelete("{id}/Dokument/{dokumentId}")]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateDokumentFürVermittler(int id, int dokumentId,
            [FromBody] SoftDeleteDokumentFürVermittlerCommand command)
        {
            if (id != command.VermittlerId)
                return BadRequest();

            if (dokumentId != command.DokumentId)
                return BadRequest();
            
            try
            {
                await Mediator.Send(command);
                return NoContent();
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

        [HttpPut("{id}")]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateVermittler(int id, [FromBody] UpdateVermittlerCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            
            try
            {
                await Mediator.Send(command);

                return NoContent();
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
    }
}