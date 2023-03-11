using Domain.Entities.IdentityEntities;
using Persistence;

namespace ZayadaAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 4;
                }
                ).AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication();
            return services;
        }
    }
}
