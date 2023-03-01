using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Gyms
{
    public class GymsList
    {
        public class Query : IRequest<IReadOnlyList<Gym>> { }

        public class Handler : IRequestHandler<Query, IReadOnlyList<Gym>>
        {
            private readonly IGenericRepository<Gym> _gymRepository;

            public Handler(IGenericRepository<Gym> gymRepository)
            {
                _gymRepository = gymRepository;
            }

            public async Task<IReadOnlyList<Gym>> Handle(Query request, CancellationToken cancellationToken)
            {
                var gyms = await _gymRepository.ListAllAsync();
                return gyms;
            }
        }
    }
}
