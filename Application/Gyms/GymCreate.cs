using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Gyms
{
    public class GymCreate
    {
        public class Command : IRequest
        {
           public GymsToPostDto Gym { get; set; }

        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly IGenericRepository<Gym> _gymRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Gym> gymRepository,IMapper mapper)
            {
                _gymRepository = gymRepository;
                _mapper = mapper;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var mappedGym = _mapper.Map<GymsToPostDto, Gym>(request.Gym);
                await _gymRepository.AddAsync(mappedGym);
                await Task.CompletedTask;
            }
        }
    }
}
