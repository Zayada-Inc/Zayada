using Application.Services.Email;
using FluentValidation;

namespace Application.CommandsQueries.Email.EmailValidator
{
    public class EmailRequestValidator: AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(x => x.ToEmail).EmailAddress().NotEmpty();
            RuleFor(x => x.Subject).NotEmpty();
        }
    }
}
