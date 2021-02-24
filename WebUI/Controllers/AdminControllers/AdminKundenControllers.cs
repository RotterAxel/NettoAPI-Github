using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.InsuranceAdmin.Query.GetKunden;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.AdminControllers
{
    public class AdminKundenController : ApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<KundenÜbersichtDto>>> GetKunden()
        {
            try
            {
                return Ok(await Mediator.Send(new GetKundenQuery()));
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }
    }
}