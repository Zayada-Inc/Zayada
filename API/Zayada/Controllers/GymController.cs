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
            var gyms = await GymRepository.ListAllAsync();
            if (gyms.Count == 0)
            {
                return NotFound(404);
            }
            var data = Mapper.Map<IReadOnlyList<Gym>, IReadOnlyList<GymsToReturnDto>>(gyms);
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
            await GymRepository.AddAsync(mappedGym);
            return Ok(mappedGym);
        }
    }
}
