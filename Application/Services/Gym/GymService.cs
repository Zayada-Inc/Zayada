using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services
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

    public class GymService : IGymService
    {
        private readonly DataContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccesor _userAccesor;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GymService(DataContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            IUserAccesor userAccesor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _userAccesor = userAccesor;
        }

        public async Task CreateGymAsync(Gym gym, Employee adminEmployee)
        {
            // Make sure the gym name is unique
            if (await _dbContext.Gyms.AnyAsync(g => g.GymName == gym.GymName))
            {
                throw new Exception("Gym name must be unique.");
            }

            // Add the gym and admin employee to the database
            if (!_roleManager.RoleExistsAsync(UserRoles.GymAdmin).Result)
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.GymAdmin));
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.GymAdmin))
            {
                _userManager.AddToRoleAsync(adminEmployee.User, UserRoles.GymAdmin).Wait();
            }

            await _dbContext.Gyms.AddAsync(gym);
            await _dbContext.SaveChangesAsync();

            adminEmployee.GymId = gym.Id;
            adminEmployee.Role = UserRoles.GymAdmin;


            await _dbContext.Employees.AddAsync(adminEmployee);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<SubscriptionPlanToReturnDto>> GetAllPlansForGymAsync(int gymId)
        {
            var gym = await _dbContext.Gyms.FindAsync(gymId);
            if (gym == null)
            {
                throw new Exception("Gym not found.");
            }

            var plans = await _dbContext.SubscriptionPlans
                .Where(p => p.GymId == gymId)
                .ToListAsync();

            var planDtos = plans.Select(p => new SubscriptionPlanToReturnDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationInDays = p.DurationInDays,
                Price = p.Price,
                GymId = p.GymId
            }).ToList();
            if (planDtos.Count == 0)
            {
                throw new Exception("No plans found for this gym.");
            }

            return planDtos;
        }
        public async Task ValidateEmployee(string userId, int gymId)
        {
            var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (userExists == null)
            {
                throw new Exception("User not found.");
            }
            var isEmployee = await _dbContext.Employees.AnyAsync(e => e.UserId == userId);
            if (isEmployee)
            {
                throw new Exception("User is already an employee.");
            }

            var gymExists = await _dbContext.Gyms.FirstOrDefaultAsync(g => g.Id == gymId);
            if (gymExists == null)
            {
                throw new Exception("Gym not found.");
            }
        }

        public async Task AddEmployeeToGymAsync(EmployeeToPostDto employee, string requestingUserId)
        {
            var hasAccess = await IsGymAdminForCurrentGymAsync(requestingUserId, employee.GymId);
            if (!hasAccess)
            {
                throw new Exception("User does not have access to add employees to this gym.");
            }
           await ValidateEmployee(employee.UserId, employee.GymId);
            var mappedEmployee = new Employee
            {
                GymId = employee.GymId,
                UserId = employee.UserId,
                Role = UserRoles.GymEmployee
            };
            _dbContext.Employees.Add(mappedEmployee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesForCurrentGymAsync()
        {
            var userId = _userAccesor.GetCurrentUsername();
            var employee = await _dbContext.Employees.SingleOrDefaultAsync(e => e.UserId == userId);

            var IsGymAdminInCurrentGym = _dbContext.Employees.Any(e => e.UserId == userId && e.Role == UserRoles.GymAdmin);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (await IsUserInRoleAsync(user, UserRoles.GymAdmin))
                if (employee == null || IsGymAdminInCurrentGym == false)
                {
                    throw new Exception("User is not an employee.");
                }


            var isGymAdmin = await IsUserInRoleAsync(user, UserRoles.GymAdmin);

            if (!isGymAdmin)
            {
                throw new Exception("User is not a gym admin.");
            }

            var gymId = employee.GymId;
            var employees = await _dbContext.Employees
                .Include(e  => e.User)
                .Include(e => e.Gym)
                .Where(e => e.GymId == gymId).ToListAsync();

            var employeeDtos = employees.Select(e => new EmployeeToReturnDto
            {
                Id = e.Id,
                Name = e.User.DisplayName,
                Role = e.Role,
                UserId = e.UserId,
                GymId = e.GymId,
                GymName = e.Gym.GymName
            });
            return employeeDtos;
        }

        public async Task<Gym> GetGymByIdAsync(int id)
        {
            return await _dbContext.Gyms.FindAsync(id);
        }

        private async Task<bool> IsUserInRoleAsync(AppUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<bool> IsGymAdminForCurrentGymAsync(string userId,int gymId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            var isSuperAdmin = await IsUserInRoleAsync(user, UserRoles.Admin);
            if (isSuperAdmin)
            {
                return true;
            }

            var userEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.UserId == userId);
            if (userEmployee == null)
            {
                return false;
            }

            var gym = await _dbContext.Gyms.FirstOrDefaultAsync(g => g.Id == userEmployee.GymId);
            if (gym == null)
            {
                return false;
            }

            var isGymAdmin = await IsUserInRoleAsync(user, UserRoles.GymAdmin);
            return isGymAdmin && userEmployee.GymId == gym.Id && userEmployee.GymId == gymId;
        }
    }
}
