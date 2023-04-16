using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Cryptography.X509Certificates;

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

        private async Task EnsureMemberRoleExistsAsync()
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
            if (await IsUserSubscribedToGym(user, gym))
            {
                throw new Exception($"User is already a member to Gym: {gym.GymName}");
            }

            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddDays(plan.DurationInDays);

            var gymMembership = new GymMembership
            {
                UserId = user.Id,
                GymId = gym.Id,
                MembershipStartDate = startDate,
                MembershipEndDate = endDate,
                PaymentAmount = plan.Price,
                SubscriptionPlanId = plan.Id
            };

            if (!await IsUserInMemberRoleAsync(user))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Member);
            }

            await _gymMembershipRepo.AddAsync(gymMembership);
            return true;
        }

        private async Task<bool> IsUserSubscribedToGym(AppUser user, Gym gym)
        {
            return await _dataContext.GymMemberships.AnyAsync(x => x.UserId == user.Id && x.GymId == gym.Id);
        }

        private async Task<bool> IsUserInMemberRoleAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(UserRoles.Member);
        }

        public async Task<bool> IsMembershipExpired(AppUser user, Gym gym)
        {
            var gymMembership = await _dataContext.GymMemberships.Where(x => x.UserId == user.Id && x.GymId == gym.Id)
                .FirstOrDefaultAsync();

            if (gymMembership == null)
            {
                return true;
            }

            return gymMembership.MembershipEndDate < DateTime.UtcNow;
        }

        public async Task<List<GymMembership>> GetUserMembershipsAsync(AppUser user)
        {
            var data = await _dataContext.GymMemberships
                .Where(gm => gm.UserId == user.Id)
                .Include(x => x.Gym)
                .ToListAsync();
            return data;
        }

        public async Task<List<AppUser>> GetGymSubscribersAsync(Gym gym)
        {
            var memberships = await _dataContext.GymMemberships
                .Include(gm => gm.User)
                .Where(gm => gm.GymId == gym.Id)
                .ToListAsync();

            return memberships.Select(gm => gm.User).ToList();
        }

        public async Task<bool> CancelMembershipAsync(AppUser user, Gym gym)
        {
            var gymMembership = await _dataContext.GymMemberships
                .Where(gm => gm.UserId == user.Id && gm.GymId == gym.Id)
                .FirstOrDefaultAsync();

            if (gymMembership == null)
            {
                throw new Exception("User does not have an active membership for the specified gym.");
            }

            _dataContext.GymMemberships.Remove(gymMembership);
            await _dataContext.SaveChangesAsync();

            return true;
        }

    }
}
