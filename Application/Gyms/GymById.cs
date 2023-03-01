using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Gyms
{
    public class GymById
    {
        public class Query : IRequest<Gym>
        {
            public int Id { get; set; }

        }

        public class Handler : IRequestHandler<Query, Gym>
        {
            private readonly IGenericRepository<Gym> _gymRepository;

            public Handler(IGenericRepository<Gym> gymRepository)
            {
                _gymRepository = gymRepository;
            }

            public async Task<Gym> Handle(Query request, CancellationToken cancellationToken)
            {
                var gym = await _gymRepository.GetByIdAsync(request.Id);
                return gym;
            }
        }
    }
}
