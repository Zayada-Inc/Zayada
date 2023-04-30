using Infrastructure.Dtos;

namespace Application.Dtos
{
    public class MembershipToReturnDto
    {
        public string MembershipStartDate { get; set; }
        public string MembershipEndDate { get; set; }
        public string GymName { get; set; }
        public SubscriptionPlanToReturnDto SubscriptionPlan { get; set; }
    }
}
