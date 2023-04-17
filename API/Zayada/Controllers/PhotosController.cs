using Application.CommandsQueries.Photos;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZayadaAPI.Errors;

namespace ZayadaAPI.Controllers
{
    public class PhotosController: BaseApiController
    {
        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddPhoto.Command command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("set-main")]
        public async Task<IActionResult> SetMain([FromForm] SetMainPhoto.Command command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }

        }
    }
}
