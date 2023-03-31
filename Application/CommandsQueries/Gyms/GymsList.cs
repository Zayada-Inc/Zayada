using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CommandsQueries.Gyms
{
    public class GymsList
    {
        public class Query : IRequest<IReadOnlyList<GymsToReturnDto>> { }

        public class Handler : IRequestHandler<Query, IReadOnlyList<GymsToReturnDto>>
        {
            private readonly IGenericRepository<Gym> _gymRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Gym> gymRepository, IMapper mapper)
            {
                _gymRepository = gymRepository;
                _mapper = mapper;
            }

            public async Task<IReadOnlyList<GymsToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var gyms = await _gymRepository.ListAllAsync();
                var data = _mapper.Map<IReadOnlyList<Gym>, IReadOnlyList<GymsToReturnDto>>(gyms);
                return data;
            }
        }
    }
}
