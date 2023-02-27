using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<int> DeleteAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
