using Application.Dtos;
using Domain.Specifications.PersonalTrainers;
using FluentValidation;

namespace Application.CommandsQueries.PersonalTrainers.Validators
{
    public class PersonalTrainerValidator : AbstractValidator<PersonalTrainersToPost>
    {
        public PersonalTrainerValidator()
        {
            RuleFor(x => x.Certifications).NotEmpty();
            RuleFor(x => x.GymId).NotEmpty().GreaterThan(0);
        }
    }

}
