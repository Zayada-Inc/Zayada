using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Specifications.PersonalTrainers;
using MediatR;

namespace Application.CommandsQueries.PersonalTrainers
{
    public class PersonalTrainersList
    {
        public class Query : IRequest<Pagination<PersonalTrainersToReturnDto>>
        {
            public PersonalTrainersParam PersonalTrainerParams { get; set; }

        }

        public class Handler : IRequestHandler<Query, Pagination<PersonalTrainersToReturnDto>>
        {
            private readonly IGenericRepository<PersonalTrainer> _personalTrainerRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<PersonalTrainer> personalTrainerRepository, IMapper mapper)
            {
                _personalTrainerRepository = personalTrainerRepository;
                _mapper = mapper;
            }

            public async Task<Pagination<PersonalTrainersToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new PersonalTrainersSpecification(request.PersonalTrainerParams);
                var countSpec = new PersonalTrainersWithFilterForCountSpecification(request.PersonalTrainerParams);
                var totalItems = await _personalTrainerRepository.CountAsync(countSpec);
                var trainers = await _personalTrainerRepository.ListAsync(spec);
                var data = _mapper.Map<IReadOnlyList<PersonalTrainer>, IReadOnlyList<PersonalTrainersToReturnDto>>(trainers);

                return new Pagination<PersonalTrainersToReturnDto>(request.PersonalTrainerParams.PageIndex, request.PersonalTrainerParams.PageSize, totalItems, data);
            }
        }
    }
}
