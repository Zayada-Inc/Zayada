using Application.Dtos;
using FluentValidation;

namespace Application.CommandsQueries.Gyms.GymValidator
{
    public class Validator : AbstractValidator<GymsToPostDto>
    {
        public Validator()
        {
            RuleFor(x => x.GymName).NotEmpty();
            RuleFor(x => x.GymAddress).NotEmpty();
        }
    }
}
