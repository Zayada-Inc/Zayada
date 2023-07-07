using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using ZayadaAPI.Errors;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities.IdentityEntities;
using Application.CommandsQueries.Gyms;
using Application.Helpers;
using Application.CommandsQueries.GymSubscriptionPlan;
using Domain.Specifications.Gyms;
using Domain.Helpers;
using Infrastructure.Dtos;

namespace ZayadaAPI.Controllers
{
    [Authorize]
    public class GymController : BaseApiController
    {

        [Cached(30)]
        [HttpGet]
        public async Task<ActionResult<Pagination<GymsToReturnDto>>> GetGyms([FromQuery]GymsParam gymsParam)
        {
            var gyms = await Mediator.Send(new GymsList.Query { GymParams = gymsParam});
            
            if (gyms.Data.Count == 0)
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
        public async Task<IActionResult> CreateGym([FromBody] GymsToPostDto gymToPostDto)
        {
            try
            {
                var result = await Mediator.Send(new GymCreate.Command { Gym = gymToPostDto });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiValidationErrorResponse(ex.Message));
            }
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
                return BadRequest(new ApiValidationErrorResponse(ex.Message));
            }
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpPut("update-gym")]
        public async Task<IActionResult> UpdateGym([FromBody] GymsToEditDto gym, [FromQuery] int id)
        {
            try
            {
                var result = await Mediator.Send(new GymEdit.Command { Gym = gym, Id = id });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiValidationErrorResponse(ex.Message));
            }
        }

        [Cached(60)]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await Mediator.Send(new GymEmployeesQuery.Query());
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiValidationErrorResponse(ex.Message));
            }
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.GymAdmin)]
        [HttpPost("subscriptionPlan")]
        public async Task<ActionResult<SubscriptionPlanToPostDto>> AddSubscriptionPlanToGym([FromQuery] SubscriptionPlanToPostDto plan)
        {
            await Mediator.Send(new GymSubscriptionPlanCreate.Command { SubscriptionPlanToPostDto = plan });
            return Ok(Task.CompletedTask);
        }

        [Cached(60)]
        [HttpGet("{gymId}/plans")]
        public async Task<ActionResult<List<SubscriptionPlanToReturnDto>>> GetAllPlansForGym(int gymId)
        {
            try
            {
                var plans = await Mediator.Send(new SubscriptionPlansForGymQuery.Query { GymId = gymId });
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiValidationErrorResponse(ex.Message));
            }

        }

    }
}
