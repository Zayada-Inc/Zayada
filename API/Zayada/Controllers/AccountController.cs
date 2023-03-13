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
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                Bio = "",
                UserName = registerDto.Username
            };

           
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, UserRoles.User);
            if (result.Succeeded)
            {
                return Ok(new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = await _tokenService.CreateToken(user),
                    Username = user.UserName
                });
            }

            return BadRequest(new ApiResponse(401,result.Errors.FirstOrDefault().Description));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (result)
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                    Token = await _tokenService.CreateToken(user),
                    Image = null
                };
            return Unauthorized(new ApiResponse(401));
        }

        [HttpPost("registerAdmin")]
        public async Task<ActionResult<UserDto>> RegisterAdmin([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            int count =  _userManager.Users.Count();
            if (userExists != null || count > 0)
                return BadRequest("User exists");

            AppUser user = new()
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Bio = "",
                UserName = model.Username
            };
            foreach (IPasswordValidator<AppUser> validator in _userManager.PasswordValidators)
            {
                IdentityResult res = await validator.ValidateAsync(_userManager, user, model.Password);
                if (!res.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, res.Errors.FirstOrDefault().Description));
                }
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest("Fail");

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

                return Ok("succes");
            
        }
    }
}
