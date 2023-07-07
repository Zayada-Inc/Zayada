using Application.Dtos;
using FluentValidation;

namespace Application.CommandsQueries.Gyms.GymValidator
{
    public class EditValidator : AbstractValidator<GymsToEditDto>
    {
        public EditValidator()
        {
            RuleFor(x => x.GymName).NotEmpty();
            RuleFor(x => x.GymAddress).NotEmpty();
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
