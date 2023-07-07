using Application.Dtos;
using Domain.Entities;
using Infrastructure.Dtos;

namespace Application.Interfaces
{
    public interface IGymService
    {
        Task AddEmployeeToGymAsync(EmployeeToPostDto employee, string requestingUserId);
        Task CreateGymAsync(Gym gym, Employee adminEmployee);
        Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesForCurrentGymAsync();
        Task<bool> IsGymAdminForCurrentGymAsync(string userId, int gymId);
        Task<Gym> GetGymByIdAsync(int id);

        Task<List<SubscriptionPlanToReturnDto>> GetAllPlansForGymAsync(int gymId);
    }
}
