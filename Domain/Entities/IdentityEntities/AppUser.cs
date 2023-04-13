using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.IdentityEntities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public PersonalTrainer PersonalTrainer { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<GymMembership> GymMemberships { get; set; }
    }
}
