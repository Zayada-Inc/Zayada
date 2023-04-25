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
using StackExchange.Redis;
using Application.Services.Cache;
using IApplication.Services.Photos;
using Application.Services.Email;
using Application.Services.Membership;
using Application.Services;
using Stripe;
using Infrastructure.Services.Payment;

namespace ZayadaAPI.Extensions
{
    public static class ApplicationServicesExtensio
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<IGymMembershipService,GymMembershipService>();
            services.AddScoped<IGymService,GymService>();
            services.AddScoped<IUserRepository,UserRepository>();
            StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable(EnvironmentVariables.StripeKey);
            services.AddScoped<IPaymentService,PaymentService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IConnectionMultiplexer>( c =>
                {
                var options = ConfigurationOptions.Parse(Environment.GetEnvironmentVariable(EnvironmentVariables.Redis), true);
                return ConnectionMultiplexer.Connect(options);
                }
                );
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
