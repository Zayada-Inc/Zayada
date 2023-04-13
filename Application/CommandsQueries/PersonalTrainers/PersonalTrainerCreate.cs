using Application.CommandsQueries.PersonalTrainers.Validators;
using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CommandsQueries.PersonalTrainers
{
    public class PersonalTrainerCreate
    {
        public class Command : IRequest
        {
            public PersonalTrainersToPost PersonalTrainer { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.PersonalTrainer).SetValidator(new PersonalTrainerValidator());
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGenericRepository<PersonalTrainer> _personalTrainerRepository;
            private readonly IMapper _mapper;
            private readonly IGymService _gymService;
            private readonly IUserAccesor _userAccesor;
            private readonly UserManager<AppUser> _userManager;
            private readonly DataContext _dataContext;

            public Handler(IGenericRepository<PersonalTrainer> personalTrainerRepository, IMapper mapper,IGymService gymService,
                IUserAccesor userAccesor, UserManager<AppUser> userManager, DataContext dataContext)
            {
                _personalTrainerRepository = personalTrainerRepository;
                _mapper = mapper;
                _gymService = gymService;
                _userAccesor = userAccesor;
                _userManager = userManager;
                _dataContext = dataContext;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var mappedTrainer = _mapper.Map<PersonalTrainersToPost, PersonalTrainer>(request.PersonalTrainer);
                var alreadyExists = await _dataContext.PersonalTrainers.AnyAsync(x => x.UserId == request.PersonalTrainer.UserId);
                if (alreadyExists)
                {
                    throw new Exception("Trainer already exists!");
                }
                await _personalTrainerRepository.AddAsync(mappedTrainer);
                var employee = new EmployeeToPostDto
                {
                    GymId = request.PersonalTrainer.GymId,
                    UserId = request.PersonalTrainer.UserId
                };
                var currentUsername = _userAccesor.GetCurrentUsername();
                await _gymService.AddEmployeeToGymAsync(employee,currentUsername);
                await Task.CompletedTask;
            }
        }
    }
}
