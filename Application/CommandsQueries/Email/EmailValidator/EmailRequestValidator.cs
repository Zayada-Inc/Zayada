using FluentValidation;
using Infrastructure.Services.Email;

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
