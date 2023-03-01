using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Gyms
{
    public class GymCreate
    {
        public class Command : IRequest
        {
           public Gym Gym { get; set; }

        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly IGenericRepository<Gym> _gymRepository;

            public Handler(IGenericRepository<Gym> gymRepository)
            {
                _gymRepository = gymRepository;
            }

            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                _gymRepository.AddAsync(request.Gym);
                return Task.CompletedTask;
            }
        }
    }
}
