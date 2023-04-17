using Domain.Entities.IdentityEntities;

namespace Domain.Entities
{
    public class Gym : EntityBase
    {   
        public string GymName { get; set; }
        public string GymAddress { get; set; }
        public ICollection<SubscriptionPlan> SubscriptionPlans { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<GymMembership> GymMemberships { get; set; }
    }
}
