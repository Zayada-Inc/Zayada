using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.DataSeeder
{
    public static class DataSeeder
    {
        public static void SeedData()
        {
        }

        public static async Task Seed(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMediator mediator,DataContext dbContext,IGenericRepository<PersonalTrainer> genericRepository, IGenericRepository<SubscriptionPlan> subscriptionRepo)
        {
            await SeedAdminUser(userManager, roleManager);
            await SeedUsers(userManager, mediator);
            await SeedGyms(userManager, roleManager, dbContext,genericRepository, subscriptionRepo);
        }

            public static async Task CreateGymAsync(DataContext _dbContext, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager, Gym gym, Employee adminEmployee)
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

        private static async Task SeedGyms(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, DataContext dbContext,IGenericRepository<PersonalTrainer> genericRepository, IGenericRepository<SubscriptionPlan> subscriptionRepo)
        {
            try
            {
                int numberOfGyms = 10;
                int employeesInGym = 20;

                var users = await dbContext.Users.Take(numberOfGyms).ToListAsync();
                var employees = await dbContext.Users.Skip(numberOfGyms).Take(employeesInGym*numberOfGyms).ToListAsync();
                for (int i = 1; i <= numberOfGyms; i++)
                {
                    var gym = new Gym
                    {
                        GymName = $"Gym{i - 1}",
                        GymAddress = $"Strada Strazilor {i - 1}",
                    };

                    var adminEmployee = new Employee
                    {
                        User = users[i - 1],
                        Gym = gym,
                        Role = UserRoles.GymAdmin
                    };

                    int personalTrainerCount = 2;
                    await CreateGymAsync(dbContext, userManager, roleManager, gym, adminEmployee);
                    await AddSubscriptionPlansToGym(i, subscriptionRepo);

                    for (int j = 1; j <= employeesInGym; j++)
                    {
                        int employeeIndex = (i - 1) * employeesInGym + (j - 1);
                        if (employeeIndex < employees.Count)
                        {
                            var employee = new EmployeeToPostDto
                            {
                                GymId = i,
                                UserId = employees[employeeIndex].Id,
                            };
                            await AddEmployeeToGymAsync(employee, users[i-1].Id, dbContext); // Updated from users to employees
                            if(personalTrainerCount>0)
                            {
                                var personalTrainer = new PersonalTrainer
                                {
                                  Certifications = $"Certificare {j}",
                                  GymId=i,
                                  UserId = employees[employeeIndex].Id,
                                };
                                await genericRepository.AddAsync(personalTrainer);
                                personalTrainerCount--;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
        }

        private static async Task AddSubscriptionPlansToGym(int id, IGenericRepository<SubscriptionPlan> subscriptionPlanRepo)
        {
            var subscriptionPlans = new List<SubscriptionPlan>
            {
                new SubscriptionPlan
                {
                    DurationInDays = 30,
                    Description = $"Plan {id}",
                    GymId = id,
                    Name = $"Basic {id}",
                    Price = 30
                },
                new SubscriptionPlan {
                    DurationInDays = 30,
                    Description = $"Plan {id}",
                    GymId = id,
                    Name = $"Medium {id}",
                    Price = 50
                },
                new SubscriptionPlan {
                    DurationInDays = 30,
                    Description = $"Plan {id}",
                    GymId = id,
                    Name = $"Pro {id}",
                    Price = 70
                }
            };

        foreach(var subscriptionPlan in subscriptionPlans)
           await subscriptionPlanRepo.AddAsync(subscriptionPlan); 
        }

        public static async Task AddEmployeeToGymAsync(EmployeeToPostDto employee, string requestingUserId, DataContext _dbContext)
        {
            var mappedEmployee = new Employee
            {
                GymId = employee.GymId,
                UserId = employee.UserId,
                Role = UserRoles.GymEmployee
            };
            _dbContext.Employees.Add(mappedEmployee);
            await _dbContext.SaveChangesAsync();
        }

        private static async Task SeedAdminUser(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var email = "admin@gmail.com";
            var displayName = "Admin";
            var username = "admin";
            var password = "Admin04";

            var userExists = await userManager.FindByEmailAsync(email);
            int count = userManager.Users.Count();

            if (userExists != null || count > 0)
                return;

            AppUser user = new()
            {
                Email = email,
                DisplayName = displayName,
                Bio = "",
                UserName = username
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return;

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            if (await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager, IMediator mediator)
        {
            int numberOfUsers = 500;

            for (int i = 1; i <= numberOfUsers; i++)
            {
                var registerDto = new RegisterDto
                {
                    DisplayName = $"User{i}",
                    Email = $"user{i}@example.com",
                    Password = $"User{i}123!",
                    Username = $"user{i}"
                };

                var userExists = await userManager.FindByEmailAsync(registerDto.Email);

                if (userExists != null)
                    continue;

                var user = new AppUser
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Bio = "",
                    UserName = registerDto.Username
                };

                var result = await userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var resultRole = await userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }
        }

    }
    public class RegisterDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }

    public class EmployeeToPostDto
    {
        public string UserId { get; set; }
        public int GymId { get; set; }
    }

    public class PersonalTrainersToPost
    {
        public string? Certifications { get; set; }
        public string UserId { get; set; }
        public int GymId { get; set; }
    }
}
