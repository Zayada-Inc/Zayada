using Application.Gyms;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;

namespace ZayadaAPI.Controllers
{
    public class GymController: BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GymsToReturnDto>>> GetGyms()
        {
            var gyms = await Mediator.Send(new GymsList.Query());
            if (gyms.Count == 0)
            {
                return NotFound(404);
            }

            return Ok(gyms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymsToReturnDto>> GetGymById(int id)
        {
            var gym = await Mediator.Send(new GymById.Query { Id = id });
            if (gym == null)
            {
                return NotFound(404);
            }

            return Ok(gym);
        }

        [HttpPost]
        public async Task<ActionResult<GymsToPostDto>> AddGym([FromQuery] GymsToPostDto gym)
        {
            if (string.IsNullOrEmpty(gym.GymName))
            {
                return BadRequest(400);
            }
            await Mediator.Send(new GymCreate.Command { Gym = gym });
            return Ok();
        }
    }
}
