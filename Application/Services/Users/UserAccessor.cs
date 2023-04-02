using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Users
{
    public class UserAccessor : IUserAccesor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor) 
        { 
           _contextAccessor = httpContextAccessor;
        }
        public string GetCurrentUsername()
        {
           return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
