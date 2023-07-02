using Application.Interfaces;
using Infrastructure.Dtos;
using MediatR;

namespace Application.CommandsQueries.GymSubscriptionPlan
{
    public class SubscriptionPlansForGymQuery
    {
        public class Query : IRequest<IReadOnlyList<SubscriptionPlanToReturnDto>>
        {
            public int GymId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IReadOnlyList<SubscriptionPlanToReturnDto>>
        {
            private readonly IGymService _gymService;
            public Handler( IGymService gymService)
            {
               _gymService = gymService;
            }

            public async Task<IReadOnlyList<SubscriptionPlanToReturnDto>> Handle(Query request, CancellationToken cancellationToken)
            {
               return await _gymService.GetAllPlansForGymAsync(request.GymId);
            }
        }
    }

}
