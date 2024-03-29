using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Data.DataSeeder;
using Persistence.Data.Repository;
using StackExchange.Redis;
using ZayadaAPI.Errors;
using ZayadaAPI.Extensions;
using ZayadaAPI.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}
);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new() { Title = "ZayadaAPI", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

    // Get the current environment.
    var env = builder.Services.BuildServiceProvider().GetRequiredService<IHostEnvironment>();

    // Construct the path to the database file.
    var dbPath =Path.GetFullPath(Path.Combine(env.ContentRootPath,"zayada.db"));

    // Set up the connection string with the correct path.
    var connectionString = $"Data Source={dbPath}";

    // Configure the rest of your services...

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlite(connectionString,
    b => b.MigrationsAssembly(nameof(Persistence))
        ));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173/").AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials().Build();
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Services.GetService<IHostApplicationLifetime>()?.ApplicationStopping.Register(() =>
{
    var connectionMultiplexer = app.Services.GetService<IConnectionMultiplexer>();
    if (connectionMultiplexer != null)
    {
        connectionMultiplexer.Close();
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    if (env.IsDevelopment())
    {
        try
        {
            bool shouldSeedData = !context.Gyms.Any();
            if (shouldSeedData)
            {
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var mediator = services.GetRequiredService<IMediator>();
                var dataContext = services.GetRequiredService<DataContext>();
                var personalTrainerRepo = services.GetRequiredService<IGenericRepository<PersonalTrainer>>();
                var subscriptionRepo = services.GetRequiredService<IGenericRepository<SubscriptionPlan>>();
                await DataSeeder.Seed(userManager, roleManager, mediator, dataContext, personalTrainerRepo, subscriptionRepo);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error in migrations" + ex.Message);
}

app.Run();
