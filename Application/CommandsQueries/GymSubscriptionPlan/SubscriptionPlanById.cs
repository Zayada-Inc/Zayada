using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Dtos;
using MediatR;

namespace Application.CommandsQueries.GymSubscriptionPlan
{
    public class SubscriptionPlanById
    {
        public class Query : IRequest<SubscriptionPlanToReturnDto>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, SubscriptionPlanToReturnDto>
        {
            private readonly IGenericRepository<Domain.Entities.SubscriptionPlan> _subscriptionPlanRepository;
            private readonly IMapper _mapper;

            public Handler(IGenericRepository<Domain.Entities.SubscriptionPlan> subscriptionPlanRepository, IMapper mapper)
            {
                _subscriptionPlanRepository = subscriptionPlanRepository;
                _mapper = mapper;
            }

            public async Task<SubscriptionPlanToReturnDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var subscriptionPlan = await _subscriptionPlanRepository.GetByIdAsync(request.Id);
                var mappedSubscriptionPlan = _mapper.Map<Domain.Entities.SubscriptionPlan, SubscriptionPlanToReturnDto>(subscriptionPlan);
                return mappedSubscriptionPlan;
            }
        }
    }
}
