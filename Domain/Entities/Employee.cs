using Domain.Entities.IdentityEntities;

namespace Domain.Entities
{
    public class Employee: EntityBase
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int GymId { get; set; }
        public Gym Gym { get; set; }
        public string Role { get; set; }
    }
}
