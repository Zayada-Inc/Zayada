using Application.Dtos;
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
    [AllowAnonymous]
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
        public async Task<ActionResult> SubscribeToGym([FromBody] SubscribeToGymToPostDto subscribeToGymDto)
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

    }
}
