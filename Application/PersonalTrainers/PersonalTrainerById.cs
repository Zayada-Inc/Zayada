using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.PersonalTrainers;
using MediatR;

namespace Application.PersonalTrainers
{
    public class PersonalTrainerById
    {
        public class Query : IRequest<PersonalTrainersToReturnDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query,PersonalTrainersToReturnDto>
        {
            private readonly IGenericRepository<PersonalTrainer> _personalTrainerRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<PersonalTrainer> personalTrainerRepository, IMapper mapper)
            {
                _personalTrainerRepository = personalTrainerRepository;
                _mapper = mapper;
            }

            public async Task<PersonalTrainersToReturnDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new PersonalTrainersSpecification(request.Id);
                var trainer = await _personalTrainerRepository.GetEntityWithSpec(spec);
                return _mapper.Map<PersonalTrainer, PersonalTrainersToReturnDto>(trainer);

            }

        }
              
    }
}
