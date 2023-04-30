using Application.CommandsQueries.PersonalTrainers;
using Application.Dtos;
using Application.Helpers;
using Domain.Entities.IdentityEntities;
using Domain.Helpers;
using Domain.Specifications.PersonalTrainers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZayadaAPI.Errors;

namespace ZayadaAPI.Controllers
{
    public class PersonalTrainerController : BaseApiController
    {
        [Cached(30)]
        [HttpGet]
        public async Task<ActionResult<Pagination<PersonalTrainersToReturnDto>>> GetTrainers([FromQuery] PersonalTrainersParam personalTrainersParam)
        {
            var trainers = await Mediator.Send(new PersonalTrainersList.Query { PersonalTrainerParams = personalTrainersParam });
            if (trainers.Data.Count == 0)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(trainers);
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpPost]
        public async Task<ActionResult<PersonalTrainersToPost>> AddTrainer([FromBody] PersonalTrainersToPost personalTrainer)
        {
            if (personalTrainer == null)
            {
                return BadRequest(new ApiResponse(400));
            }

            try
            {
                await Mediator.Send(new PersonalTrainerCreate.Command { PersonalTrainer = personalTrainer });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
            return Ok();
        }

        [Cached(30)]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalTrainersToReturnDto>> GetTrainerById(string id)
        {
            var trainer = await Mediator.Send(new PersonalTrainerById.Query { IdString = id });
            if (trainer == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(trainer);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrainer(int id)
        {
            if (id >= 0)
            {
                await Mediator.Send(new PersonalTrainerDelete.Command { Id = id });
                return Ok();
            }
            return BadRequest(new ApiResponse(400));
        }
    }
}

