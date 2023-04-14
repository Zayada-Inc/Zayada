using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.Membership
{
    public class GymMembershipService : IGymMembershipService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGenericRepository<GymMembership> _gymMembershipRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _dataContext;

        public GymMembershipService(UserManager<AppUser> userManager, IGenericRepository<GymMembership> gymMembershipRepo, RoleManager<IdentityRole> roleManager, DataContext dataContext)
        {
            _userManager = userManager;
            _gymMembershipRepo = gymMembershipRepo;
            _roleManager = roleManager;
            _dataContext = dataContext;
        }

        public async Task EnsureMemberRoleExistsAsync()
        {
            if (!await _roleManager.RoleExistsAsync(UserRoles.Member))
            {
                var role = new IdentityRole(UserRoles.Member);
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task<bool> SubscribeToGym(AppUser user, SubscriptionPlan plan)
        {
            await EnsureMemberRoleExistsAsync();

            var gym = await _dataContext.Gyms.FindAsync(plan.GymId);

            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(plan.DurationInDays);

            var gymMembership = new GymMembership
            {
                UserId = user.Id,
                GymId = gym.Id,
                MembershipStartDate = startDate,
                MembershipEndDate = endDate,
                PaymentAmount = plan.Price,
                SubscriptionPlanId = plan.Id
            };

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(UserRoles.Member))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Member);
            }
            else
            {
                var membership = await _dataContext.GymMemberships.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (membership != null)
                {

                    throw new Exception($"User is already a member to Gym: {membership.Gym.GymName}");
                }
                else
                {
                    throw new Exception("User is a member but does not have a subscription!");
                }
            }

            await _gymMembershipRepo.AddAsync(gymMembership);

            return true;
        }
        public async Task<bool> IsMembershipExpired(AppUser user, Gym gym)
        {
            var gymMembership = await _dataContext.GymMemberships.Where(x => x.UserId == user.Id && x.GymId == gym.Id)
                .FirstOrDefaultAsync(); //TO DO: Add a specification for this

            if (gymMembership == null)
            {
                return true;
            }

            if (gymMembership.MembershipEndDate < DateTime.UtcNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
