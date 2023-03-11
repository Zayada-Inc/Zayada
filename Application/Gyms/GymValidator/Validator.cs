using Application.Dtos;
using FluentValidation;

namespace Application.Gyms.GymValidator
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
