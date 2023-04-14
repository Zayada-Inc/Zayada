using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using ZayadaAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities.IdentityEntities;
using Application.CommandsQueries.Gyms;
using Application.Helpers;
using Application.CommandsQueries.GymSubscriptionPlan;
using Domain.Entities;

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

        /*
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
        */
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateGym([FromQuery]GymsToPostDto gymToPostDto)
        { 
            var result = await Mediator.Send( new GymCreate.Command { Gym = gymToPostDto});

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployeeToGym([FromBody] EmployeeToPostDto employee)
        {
            try
            {
                var result = await Mediator.Send(new GymAddEmployee.Command { Employee = employee });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiException(400,ex.Message));
            }
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await GymService.GetEmployeesForCurrentGymAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpPost("subscriptionPlan")]
        public async Task<ActionResult<SubscriptionPlanToPostDto>> AddSubscriptionPlanToGym([FromQuery] SubscriptionPlanToPostDto plan)
        {
                await Mediator.Send(new GymSubscriptionPlanCreate.Command { SubscriptionPlanToPostDto = plan });
                return Ok(Task.CompletedTask);
        }

        [HttpGet("{gymId}/plans")]
        public async Task<ActionResult<List<SubscriptionPlanToReturnDto>>> GetAllPlansForGym(int gymId)
        {
            var plans = await GymService.GetAllPlansForGymAsync(gymId);
            return Ok(plans);
        }

    }
}
