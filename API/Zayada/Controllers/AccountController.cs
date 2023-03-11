using Application.Dtos;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayadaAPI.Errors;
using ZayadaAPI.Services;

namespace ZayadaAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager,TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromQuery]RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email taken");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                Bio = "",
                UserName = registerDto.Username
            };
            var result = await _userManager.CreateAsync(user,registerDto.Password);

            if(result.Succeeded)
            {
                return Ok(new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = _tokenService.CreateToken(user),
                    Username = user.UserName
                });
            }
            return Unauthorized(new ApiResponse(401));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromQuery]LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _userManager.CheckPasswordAsync(user,loginDto.Password);

            if (result)
                return new UserDto
                {
                 DisplayName = user.DisplayName,
                 Username = user.UserName,
                 Token = _tokenService.CreateToken(user),
                 Image = null
                };
            return Unauthorized(new ApiResponse(401));
        }


    }
}
