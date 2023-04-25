using Application.Interfaces;
using Domain.Entities.IdentityEntities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CommandsQueries.Payment
{
    public class ConfirmPurchaseQuery
    {
        public class Query : IRequest<string>
        {
            public bool Success { get; set; }
            public string Token { get; set; }
            public string UserId { get; set; }
            public int PlanId { get; set; }
        }

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly DataContext _dataContext;
            private readonly UserManager<AppUser> _userManager;
            private readonly IGymMembershipService _gymMembershipService;

            public Handler(DataContext dataContext,IGymMembershipService gymMembershipService, UserManager<AppUser> userManager)
            {
                _dataContext = dataContext;
                _userManager = userManager;
                _gymMembershipService = gymMembershipService;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!request.Success)
                {
                    return "Payment failed or was canceled.";
                }

                var paymentToken = await _dataContext.PaymentTokens.SingleOrDefaultAsync(pt => pt.Token == request.Token);

                if (paymentToken == null || paymentToken.IsUsed || paymentToken.UserId != request.UserId)
                {
                    return "Invalid or expired token.";
                }

                paymentToken.IsUsed = true;
                await _dataContext.SaveChangesAsync();

                var plan = await _dataContext.SubscriptionPlans.SingleOrDefaultAsync(p => p.Id == request.PlanId);
                var user = await _userManager.FindByIdAsync(request.UserId);
                _gymMembershipService.SubscribeToGym(user, plan);

                return "Payment successful";
            }
        }
    }
}
