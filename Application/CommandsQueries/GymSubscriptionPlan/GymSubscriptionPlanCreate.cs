using Application.Dtos;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Application.CommandsQueries.GymSubscriptionPlan.GymSubscriptionPlanValidator;
using Persistence;
using Application.Services;
using Application.Interfaces;

namespace Application.CommandsQueries.GymSubscriptionPlan
{
    public class GymSubscriptionPlanCreate
    {
      public class Command : IRequest
        {
            public SubscriptionPlanToPostDto SubscriptionPlanToPostDto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SubscriptionPlanToPostDto).SetValidator(new Validator());
            }
        }   

        public class Handler : IRequestHandler<Command>
        {
            private readonly IGenericRepository<Domain.Entities.SubscriptionPlan> _subscriptionPlanRepository;
            private readonly IMapper _mapper;
            private readonly IGymService _gymService;
            private readonly IUserAccesor _userAccesor;

            public Handler(IGenericRepository<Domain.Entities.SubscriptionPlan> subscriptionPlanRepository, IMapper mapper,
                IGymService gymService, IUserAccesor userAccesor)
            {
                _subscriptionPlanRepository = subscriptionPlanRepository;
                _mapper = mapper;
                _gymService = gymService;
                _userAccesor = userAccesor;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _userAccesor.GetCurrentUsername();

                if (!await _gymService.IsGymAdminForCurrentGymAsync(userId,request.SubscriptionPlanToPostDto.GymId))
                {
                    throw new Exception("User does not have permission to create gym subscription plans.");
                }
                var mappedSubscriptionPlan = _mapper.Map<SubscriptionPlanToPostDto, Domain.Entities.SubscriptionPlan>(request.SubscriptionPlanToPostDto);
                await _subscriptionPlanRepository.AddAsync(mappedSubscriptionPlan);
                await Task.CompletedTask;
            }
        }
    }
}
