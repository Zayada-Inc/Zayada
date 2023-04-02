using Application.CommandsQueries.Photos;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /*
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }
        [HttpPost("{id}/setMain")]
        public async Task<ActionResult<Unit>> SetMain(int id)
        {
            return await Mediator.Send(new SetMain.Command { Id = id });
        }
        */
    }
}
