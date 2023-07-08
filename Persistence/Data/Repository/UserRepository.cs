using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> DeleteUser(string id)
        {
          var existingUser = await _context.Users.FindAsync(id);
                if (existingUser != null)
                {
                    _context.Users.Remove(existingUser);
                    await _context.SaveChangesAsync();
                    return existingUser;
                }
   
            
                    return null;
            
        }

        public async Task<IReadOnlyList<AppUser>> ListAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IReadOnlyList<AppUser>> ListAsync(ISpecification<AppUser> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<AppUser> UpdateUserAsync(AppUser user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return existingUser;
            }
            else
            {
                return null;
            }
        }


        public async Task<AppUser> GetEntityWithSpec(ISpecification<AppUser> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        private IQueryable<AppUser> ApplySpecification(ISpecification<AppUser> spec)
        {
            return SpecificationUserEvaluator.GetQuery(_context.Users.AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<AppUser> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

    }

    public class SpecificationUserEvaluator
    {
        public static IQueryable<AppUser> GetQuery(IQueryable<AppUser> inputQuery, ISpecification<AppUser> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
