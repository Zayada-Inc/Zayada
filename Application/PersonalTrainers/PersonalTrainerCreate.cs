using Application.Dtos;
using Application.PersonalTrainers.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.PersonalTrainers
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

            public Handler(IGenericRepository<PersonalTrainer> personalTrainerRepository, IMapper mapper)
            {
                _personalTrainerRepository = personalTrainerRepository;
                _mapper= mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var mappedTrainer = _mapper.Map<PersonalTrainersToPost,PersonalTrainer>(request.PersonalTrainer);
                await _personalTrainerRepository.AddAsync(mappedTrainer);
                await Task.CompletedTask;
            }
        }   
    }
}
