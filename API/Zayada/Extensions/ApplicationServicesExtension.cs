using Domain.Interfaces;
using Persistence.Data.Repository;
using Application.Helpers;
using FluentValidation.AspNetCore;
using FluentValidation;
using Application.CommandsQueries.Gyms;
using Application.Services.Photos.Interfaces;
using Application.Services.Photos;
using Application.Services.Users;
using Application.Interfaces;

namespace ZayadaAPI.Extensions
{
    public static class ApplicationServicesExtensio
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GymsList.Handler).Assembly));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(GymCreate));
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccesor,UserAccessor>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();

        }
    }
}
