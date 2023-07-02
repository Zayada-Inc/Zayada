using Application.CommandsQueries.Gyms.GymValidator;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Gyms
{
    public class GymCreate
    {
        public class Command : IRequest<GymsToPostDto>
        {
            public GymsToPostDto Gym { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Gym).SetValidator(new Validator());
            }
        }
        public class Handler : IRequestHandler<Command, GymsToPostDto>
        {
            private readonly IGymService _gymService;
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public Handler(IGymService gymService, UserManager<AppUser> userManager, IMapper mapper)
            {
                _gymService = gymService;
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<GymsToPostDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var gymDto = request.Gym;
                var gym = new Gym
                {
                    GymName = gymDto.GymName,
                    GymAddress = gymDto.GymAddress
                };

                var adminUser = await _userManager.FindByIdAsync(gymDto.AdminUserId);

                if (adminUser == null)
                {
                    throw new Exception("Failed to find gym admin user.");
                }

                var employee = new Employee
                {
                    Gym = gym,
                    User = adminUser,
                    Role = UserRoles.GymAdmin
                };

                await _gymService.CreateGymAsync(gym, employee);

                return gymDto;
            }


        }
    }
}
