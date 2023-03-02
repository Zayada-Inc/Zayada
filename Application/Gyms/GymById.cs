using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Gyms
{
    public class GymById
    {
        public class Query : IRequest<GymsToReturnDto>
        {
            public int Id { get; set; }

        }

        public class Handler : IRequestHandler<Query, GymsToReturnDto>
        {
            private readonly IGenericRepository<Gym> _gymRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Gym> gymRepository,IMapper mapper)
            {
                _gymRepository = gymRepository;
                _mapper = mapper;
            }

            public async Task<GymsToReturnDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var gym = await _gymRepository.GetByIdAsync(request.Id);
                var data = _mapper.Map<Gym, GymsToReturnDto>(gym);
                return data;
            }
        }
    }
}
