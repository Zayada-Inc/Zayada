using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.Membership
{
    public class GymMembershipService: IGymMembershipService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGenericRepository<GymMembership> _gymMembershipRepo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _dataContext;

        public GymMembershipService(UserManager<AppUser> userManager, IGenericRepository<GymMembership> gymMembershipRepo, RoleManager<IdentityRole> roleManager,DataContext dataContext)
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

        public async Task<bool> SubscribeToGym(AppUser user, Domain.Entities.SubscriptionPlan plan)
        {
            await EnsureMemberRoleExistsAsync();

            Gym gym = plan.Gym;

            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = startDate.AddDays(plan.DurationInDays);

            var gymMembership = new GymMembership
            {
                UserId = user.Id,
                User = user,
                GymId = gym.Id,
                Gym = gym,
                MembershipStartDate = startDate,
                MembershipEndDate = endDate,
                PaymentAmount = plan.Price
            };

            await _gymMembershipRepo.AddAsync(gymMembership);

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains(UserRoles.Member))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Member);
            }

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
