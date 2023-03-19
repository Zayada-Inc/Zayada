using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.PersonalTrainers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.PersonalTrainers
{
    public class PersonalTrainerById
    {
        public class Query : IRequest<PersonalTrainersToReturnDto>
        {
         //   public int Id { get; set; }
            public string  IdString { get; set; }
        }

        public class Handler : IRequestHandler<Query,PersonalTrainersToReturnDto>
        {
            private readonly IGenericRepository<PersonalTrainer> _personalTrainerRepository;
            private readonly IMapper _mapper;
            private readonly DataContext _dbContext;

            public Handler(IGenericRepository<PersonalTrainer> personalTrainerRepository, IMapper mapper, DataContext dbContext)
            {
                _personalTrainerRepository = personalTrainerRepository;
                _mapper = mapper;
                _dbContext = dbContext;
            }
            /*

            public async Task<PersonalTrainersToReturnDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new PersonalTrainersSpecification(request.Id);
                var trainer = await _personalTrainerRepository.GetEntityWithSpec(spec);
                return _mapper.Map<PersonalTrainer, PersonalTrainersToReturnDto>(trainer);

            }
            */
            public async Task<PersonalTrainersToReturnDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var trainer = await _dbContext
                    .PersonalTrainers
                    .Include(x => x.Gym)
                    .FirstOrDefaultAsync(x => x.UserId == request.IdString)
                    
                    ;
                return _mapper.Map<PersonalTrainer, PersonalTrainersToReturnDto>(trainer);

            }

        }
              
    }
}
