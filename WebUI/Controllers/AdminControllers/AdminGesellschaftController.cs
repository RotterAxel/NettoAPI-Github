using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.CreateGesellschaft;
using Application.InsuranceAdmin.Commands.UpdateGesellschaft;
using Application.InsuranceAdmin.Query.GetGesellschaften;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.AdminControllers
{
    public class AdminGesellschaftController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IList<GesellschaftÜbersichtDto>>> GetGesellschaften()
        {
            try
            {
                return Ok(await Mediator.Send(new GetGesellschaftenQuery()));
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
        
        [HttpPost]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> CreateGesellschafft([FromBody] CreateGesellschaftCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{gesellschaftId}")]
        [Authorize]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateGesellschafft([FromBody] UpdateGesellschaftCommand command,
             int gesellschaftId)
        {
            if (gesellschaftId != command.Id)
                return BadRequest("Url Id does not match command ID.");

            try
            {
                await Mediator.Send(command);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception);
            }
        }
    }
}