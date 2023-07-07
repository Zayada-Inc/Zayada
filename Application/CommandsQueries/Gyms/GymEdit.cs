using Application.CommandsQueries.Gyms.GymValidator;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.CommandsQueries.Gyms
{
    public class GymEdit
    {
        public class Command : IRequest<GymsToEditDto>
        {
            public GymsToEditDto Gym { get; set; }

            public int Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator() 
            { 
                RuleFor(x => x.Gym).SetValidator(new EditValidator());
            }
        }

        public class Handler : IRequestHandler<Command, GymsToEditDto>
        {
            private readonly IGymService _gymService;
            private readonly IMapper _mapper;

            public Handler(IGymService gymService, IMapper mapper)
            {
                _gymService = gymService;
                _mapper = mapper;
            }

            public async Task<GymsToEditDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var gymDto = request.Gym;
                var gym = await _gymService.GetGymByIdAsync(request.Id);

                if (gym == null)
                {
                    throw new Exception("Gym not found.");
                }

                var mapData = _mapper.Map<Gym>(gymDto);
                mapData.Id = gym.Id;
                gym.GymName = mapData.GymName ?? gym.GymName;
                gym.GymAddress = mapData.GymAddress ?? gym.GymAddress;

                var result = await _gymService.UpdateGymAsync(mapData);
                var mappedResult = _mapper.Map<GymsToEditDto>(result);
                return mappedResult;
            }
        }
    }
}
