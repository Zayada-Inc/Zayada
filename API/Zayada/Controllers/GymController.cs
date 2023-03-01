using Application.Gyms;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ZayadaAPI.Dtos;

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
            var data = Mapper.Map<IReadOnlyList<Gym>, IReadOnlyList<GymsToReturnDto>>(gyms);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymsToReturnDto>> GetGymById(int id)
        {
            var gym = await Mediator.Send(new GymById.Query { Id = id });
            if (gym == null)
            {
                return NotFound(404);
            }
            var data = Mapper.Map<Gym, GymsToReturnDto>(gym);
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<GymsToPostDto>> AddGym([FromQuery] GymsToPostDto gym)
        {
            var mappedGym = Mapper.Map<GymsToPostDto, Gym>(gym);
            if (string.IsNullOrEmpty(gym.GymName))
            {
                return BadRequest(400);
            }
            await Mediator.Send(new GymCreate.Command { Gym = mappedGym });
            return Ok();
        }
    }
}
