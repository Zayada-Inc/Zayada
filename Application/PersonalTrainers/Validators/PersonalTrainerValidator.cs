using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PersonalTrainers.Validators
{
    public class PersonalTrainerValidator : AbstractValidator<PersonalTrainersToPost>
    {
        public PersonalTrainerValidator() {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.InstagramLink).NotEmpty();
            RuleFor(x => x.Certifications).NotEmpty();
            RuleFor(x => x.GymId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ImageUrl).NotEmpty();
        }
    }
}
