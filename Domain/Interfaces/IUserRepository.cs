using Domain.Entities.IdentityEntities;

namespace Domain.Interfaces
{

    public interface IUserRepository
        {
            Task<int> CountAsync(ISpecification<AppUser> spec);
            Task<AppUser> GetByIdAsync(string id);
            Task<AppUser> GetEntityWithSpec(ISpecification<AppUser> spec);
            Task<IReadOnlyList<AppUser>> ListAllAsync();
            Task<IReadOnlyList<AppUser>> ListAsync(ISpecification<AppUser> spec);
        }
   
}
