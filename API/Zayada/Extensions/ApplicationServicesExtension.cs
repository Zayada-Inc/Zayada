using Application.Gyms;
using Domain.Interfaces;
using Persistence.Data.Repository;
using Application.Helpers;

namespace ZayadaAPI.Extensions
{
    public static class ApplicationServicesExtensio
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GymsList.Handler).Assembly));


        }
    }
}
