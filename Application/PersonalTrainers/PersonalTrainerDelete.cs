using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.PersonalTrainers;
using MediatR;

namespace Application.PersonalTrainers
{
    public class PersonalTrainerDelete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGenericRepository<PersonalTrainer> _personalTrainerRepo;

            public Handler(IGenericRepository<PersonalTrainer> repository)
            {
                _personalTrainerRepo = repository;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var spec = new PersonalTrainersSpecification(request.Id);
                var trainer = await _personalTrainerRepo.GetEntityWithSpec(spec);
                if(trainer != null)
                {
                  await  _personalTrainerRepo.DeleteAsync(spec);
                }

                await Task.CompletedTask;
            }
        }
    }
}
