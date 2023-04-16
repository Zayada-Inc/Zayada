using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Data.Common;

namespace ZayadaAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MembershipController: ControllerBase
    {
        private readonly IGymMembershipService _gymMembershipService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccesor _userAccesor;
        private readonly DataContext _dataContext;

        public MembershipController(IGymMembershipService gymMembershipService, UserManager<AppUser> userManager,IUserAccesor userAccesor,
            DataContext dataContext)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _userAccesor = userAccesor;
            _gymMembershipService = gymMembershipService;
        }

        [HttpPost("subscribeToGym")]
        public async Task<ActionResult> SubscribeToGym([FromBody] SubscribeToGymToPostDto subscribeToGymDto) //TO DO: move into service/ use CQRS   
        {
            var userId =  _userAccesor.GetCurrentUsername();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var plan = await _dataContext.SubscriptionPlans.FindAsync(subscribeToGymDto.GymSubscriptionPlanId);

            
            if (plan == null)
            {
                return NotFound();
            }

            var result = await _gymMembershipService.SubscribeToGym(user, plan);
            if (result)
            {
                return Ok();
            }

            return BadRequest("Problem subscribing to gym");
        }

        [Cached(30)]
        [HttpGet("getMembership")]
        public async Task<IActionResult> GetUserMembership() //TO DO: move into gym service, use CQRS   
        {
            var user = await _userManager.FindByIdAsync(_userAccesor.GetCurrentUsername());
            var memberships = await _gymMembershipService.GetUserMembershipsAsync(user);
            var gymMembershipsDto = new List<MembershipToReturnDto>();
            foreach (var membership in memberships)
            {
                gymMembershipsDto.Add(new MembershipToReturnDto
                {
                    GymName = membership.Gym.GymName,
                    MembershipStartDate = membership.MembershipStartDate.ToShortDateString(),
                    MembershipEndDate = membership.MembershipEndDate.ToShortDateString(),

                });
            }
            return Ok(gymMembershipsDto);
        }
    }
}
