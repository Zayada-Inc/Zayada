using Application.CommandsQueries.Payment;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;

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
        private readonly IMediator _mediator;

        public MembershipController(IGymMembershipService gymMembershipService, UserManager<AppUser> userManager,IUserAccesor userAccesor,
            DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _userAccesor = userAccesor;
            _gymMembershipService = gymMembershipService;
            _mediator = mediator;
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
                    SubscriptionPlan = new()
                    {
                        Id = membership.SubscriptionPlanId,
                        GymId = membership.SubscriptionPlanId,
                        Name = membership.SubscriptionPlan.Name,
                        Price = membership.SubscriptionPlan.Price,
                        Description = membership.SubscriptionPlan.Description,
                        DurationInDays = membership.SubscriptionPlan.DurationInDays,
                    }
                });
            }
            return Ok(gymMembershipsDto);
        }


        [Authorize]
        [HttpPost("purchase-subscription-plan/{planId}")]
        public async Task<ActionResult<string>> PurchaseSubscriptionPlan(int planId)
        {
            string checkoutSessionUrl = await _mediator.Send(new PurchaseSubscriptionPlanCommand.Command { PlanId = planId });
            return Ok(new { url = checkoutSessionUrl });
        }

        [AllowAnonymous]
        [HttpGet("confirm-purchase")]
        public async Task<ActionResult<string>> ConfirmPurchase([FromQuery] bool success, string token, string userId, int planId)
        {
            string result = await _mediator.Send(new ConfirmPurchaseQuery.Query { Success = success, Token = token, UserId = userId, PlanId = planId });

            if (result == "Payment failed or was canceled." || result == "Invalid or expired token.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
