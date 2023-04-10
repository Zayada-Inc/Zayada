using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using ZayadaAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities.IdentityEntities;
using Application.CommandsQueries.Gyms;
using Application.Helpers;

namespace ZayadaAPI.Controllers
{
    public class GymController: BaseApiController
    {
        [Cached(30)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GymsToReturnDto>>> GetGyms()
        {
            var gyms = await Mediator.Send(new GymsList.Query());
            if (gyms.Count == 0)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(gyms);
        }

        [Cached(30)]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GymsToReturnDto>> GetGymById(int id)
        {
            var gym = await Mediator.Send(new GymById.Query { Id = id });
            if (gym == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(gym);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<GymsToPostDto>> AddGym([FromQuery] GymsToPostDto gym)
        {
            if (string.IsNullOrEmpty(gym.GymName))
            {
                return BadRequest(new ApiResponse(400));
            }
            await Mediator.Send(new GymCreate.Command { Gym = gym });
            return Ok(Task.CompletedTask);
        }
    }
}
