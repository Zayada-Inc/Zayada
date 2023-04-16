using Application.Dtos;
using FluentValidation;

namespace Application.CommandsQueries.GymSubscriptionPlan.GymSubscriptionPlanValidator
{

        public class Validator : AbstractValidator<SubscriptionPlanToPostDto>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Price).NotEmpty();
                RuleFor(x => x.DurationInDays).NotEmpty();
                RuleFor(x => x.GymId).NotEmpty();
            }
        }
    }

