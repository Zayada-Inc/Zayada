using Domain.Entities;
using Domain.Entities.IdentityEntities;

namespace Application.Interfaces
{
    public interface IGymMembershipService
    {
        Task<bool> CancelMembershipAsync(AppUser user, Gym gym);
        Task<List<AppUser>> GetGymSubscribersAsync(Gym gym);
        Task<List<GymMembership>> GetUserMembershipsAsync(AppUser user);
        Task<bool> IsMembershipExpired(AppUser user, Gym gym);
        Task<bool> SubscribeToGym(AppUser user, SubscriptionPlan plan);

        Task<bool> IsUserSubscribedToGym(AppUser user, Gym gym);
    }
}