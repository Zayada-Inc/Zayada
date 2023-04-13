using Domain.Entities;
using Domain.Entities.IdentityEntities;

namespace Domain.Interfaces
{
    public interface IGymMembershipService
    {
        Task EnsureMemberRoleExistsAsync();
        Task<bool> IsMembershipExpired(AppUser user, Gym gym);
        Task<bool> SubscribeToGym(AppUser user, SubscriptionPlan plan);
    }
}