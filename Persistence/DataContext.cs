using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PersonalTrainer> PersonalTrainers { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
