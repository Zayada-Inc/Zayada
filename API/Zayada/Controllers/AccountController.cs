﻿using Application.CommandsQueries.Email;
using Application.CommandsQueries.PersonalTrainers;
using Application.CommandsQueries.Photos;
using Application.Dtos;
using Application.Helpers;
using Application.Services.Email;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZayadaAPI.Errors;
using ZayadaAPI.Services;
using IdentityError = ZayadaAPI.Errors.IdentityError;

namespace ZayadaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMediator _mediator;
        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, RoleManager<IdentityRole> roleManager, IMediator mediator, IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _mediator = mediator;
            _emailService = emailService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = new AppUser
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Bio = "",
                    UserName = registerDto.Username
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var resultRole = await _userManager.AddToRoleAsync(user, UserRoles.User);

                    if (resultRole.Succeeded)
                    {
                        return Ok(new UserDto
                        {
                            DisplayName = user.DisplayName,
                            Photos = null,
                            Token = await _tokenService.CreateToken(user),
                            Username = user.UserName,
                            // PersonalTrainer = null

                        });
                    }
                }
                return BadRequest(IdentityError.Response(result));
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiValidationErrorResponse { Errors = new List<string> { ex.Message } });
            }

            
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            return Ok(await _mediator.Send(new EmailCreate.Command { EmailRequest = emailRequest }));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                    return Unauthorized(new ApiValidationErrorResponse { Errors = new List<string> { "Wrong email or password! " } });
                var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (result)
                    return new UserDto
                    {
                        DisplayName = user.DisplayName,
                        Username = user.UserName,
                        Token = await _tokenService.CreateToken(user),
                        Photos = _mediator.Send(new PhotosByUserId.Query { UserId = user.Id }).Result
                    };
                return Unauthorized(new ApiValidationErrorResponse { Errors = new List<string> { "Wrong email or password! " } });
            }

            catch (Exception ex)
            {
                return BadRequest(new ApiValidationErrorResponse { Errors = new List<string> { ex.Message } });
            }
        }

        

        [AllowAnonymous]
        [HttpPost("registerAdmin")]
        public async Task<ActionResult<UserDto>> RegisterAdmin([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            int count = _userManager.Users.Count();
            if (userExists != null || count > 0)
                return BadRequest(new ApiValidationErrorResponse { Errors = new List<string> { "User Exists! " } });

            AppUser user = new()
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Bio = "",
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(IdentityError.Response(result));

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

            return Ok(result);

        }

        [Cached(30)]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<List<UserReturnDto>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
           
            var mappedUsers = users.Select(async user => new UserReturnDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Email = user.Email,
                Image = null,
                PersonalTrainer = await _mediator.Send(new PersonalTrainerById.Query { IdString = user.Id }),
                Photos = await _mediator.Send(new PhotosByUserId.Query { UserId = user.Id })
            }).ToList();

            return Ok(mappedUsers.AsEnumerable().Select(x => x.Result).ToList());
        }

        public class UserReturnDto
            {
                public string Id { get; set; }
                public string DisplayName { get; set; } 
                public string Email { get; set; }
                public string Username { get; set; }
                public string Image { get; set; }
                public PersonalTrainersToReturnDto PersonalTrainer { get; set; }
                public IEnumerable<Photo> Photos { get; set; }
            }
    }
}
