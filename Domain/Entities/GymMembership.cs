using Domain.Entities.IdentityEntities;

namespace Domain.Entities
{
    public class GymMembership: EntityBase
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public double PaymentAmount { get; set; }
        public int SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
