using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Specifications.Gyms;
using MediatR;

namespace Application.CommandsQueries.Gyms
{
    public class GymsList
    {
        public class Query : IRequest<Pagination<GymsToReturnDto>> 
        {
            public GymsParam GymParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<GymsToReturnDto>>
        {
            private readonly IGenericRepository<Gym> _gymRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Gym> gymRepository, IMapper mapper)
            {
                _gymRepository = gymRepository;
                _mapper = mapper;
            }

            public async Task<Pagination<GymsToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
            {
               var spec = new GymsSpecification(request.GymParams);
               var countSpec = new GymsWithFilterForCountSpecification(request.GymParams);
               var totalItems = await _gymRepository.CountAsync(countSpec);
               var gyms = await _gymRepository.ListAsync(spec);
               var data = _mapper.Map<IReadOnlyList<Gym>, IReadOnlyList<GymsToReturnDto>>(gyms);

                return new Pagination<GymsToReturnDto>(request.GymParams.PageIndex, request.GymParams.PageSize, totalItems, data);
            }
        }
    }
}
