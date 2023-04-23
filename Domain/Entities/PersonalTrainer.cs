using Domain.Entities.IdentityEntities;

namespace Domain.Entities
{
    public class PersonalTrainer : EntityBase
    {
        public string? Certifications { get; set; }
        public int? GymId { get; set; }
        public Gym Gym { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
 