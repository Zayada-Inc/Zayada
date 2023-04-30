using Application.CommandsQueries.GymSubscriptionPlan;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Infrastructure.Services.Payment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.CommandsQueries.Payment
{
    public class PurchaseSubscriptionPlanCommand
    {
        public class Command : IRequest<string>
        {
            public int PlanId { get; set; }
        }

        public class Handler : IRequestHandler<Command, string>
        {
            private readonly IMediator _mediator;
            private readonly UserManager<AppUser> _userManager;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly DataContext _dataContext;
            private readonly IGymMembershipService _gymMembershipService;
            private readonly IUserAccesor _userAccesor;
            private readonly IPaymentService _paymentService;

            public Handler(IMediator mediator, IHttpContextAccessor httpContextAccessor, DataContext dataContext, IPaymentService paymentService,IUserAccesor userAccesor,IGymMembershipService gymMembershipService,UserManager<AppUser> userManager)
            {
                _mediator = mediator;
                _userAccesor = userAccesor;
                _userManager = userManager;
                _gymMembershipService = gymMembershipService;
                _httpContextAccessor = httpContextAccessor;
                _dataContext = dataContext;
                _paymentService = paymentService;
                _userAccesor = userAccesor;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                var plan = await _mediator.Send(new SubscriptionPlanById.Query { Id = request.PlanId });
                string token = Guid.NewGuid().ToString();
                string userId = _userAccesor.GetCurrentUsername();

                var user = await _userManager.FindByIdAsync(userId);
                var gym = _dataContext.Gyms.FirstOrDefault(x => x.Id == plan.GymId);

                var isUserSubscribed = await _gymMembershipService.IsUserSubscribedToGym(user, gym);
                var isMembershipExpired = await _gymMembershipService.IsMembershipExpired(user, gym);
                if ((isUserSubscribed == true) && (isMembershipExpired == false))
                    throw new Exception("User is already member to this gym!");

                var paymentToken = new PaymentToken // To do: add expiration time
                {
                    Token = token,
                    UserId = userId,
                    IsUsed = false,
                };

                _dataContext.PaymentTokens.Add(paymentToken);
                await _dataContext.SaveChangesAsync();

                string returnUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Membership/confirm-purchase?token={token}&userId={userId}&planId={plan.Id}";
                string checkoutSessionUrl = await _paymentService.CreateCheckoutSessionAsync(plan, returnUrl);

                return checkoutSessionUrl;
            }
        }
    }
}
