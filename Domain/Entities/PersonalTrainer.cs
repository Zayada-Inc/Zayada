using Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PersonalTrainer : EntityBase
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? InstagramLink { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Certifications { get; set; }
        public int? GymId { get; set; }
        public Gym Gym { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
 