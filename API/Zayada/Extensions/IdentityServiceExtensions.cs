using Domain.Entities.IdentityEntities;
using Infrastructure.Services.Photos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Net;
using System.Text;
using System.Text.Json;
using ZayadaAPI.Errors;
using ZayadaAPI.Services;

namespace ZayadaAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = true;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireDigit = true;
                    opt.Password.RequiredLength = 4;
                }
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(EnvironmentVariables.ZayadaApiKey))),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            var response = new ApiResponse((int)HttpStatusCode.Unauthorized);
                            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                            var json = JsonSerializer.Serialize(response, options);

                            return context.Response.WriteAsync(json);
                        },
                        OnForbidden = context =>
                        {    
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            var response = new ApiResponse((int)HttpStatusCode.Forbidden);
                            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                            var json = JsonSerializer.Serialize(response, options);

                            return context.Response.WriteAsync(json);
                        }
                    };
                });
            services.AddScoped<TokenService>();
            return services;
        }
    }
}
