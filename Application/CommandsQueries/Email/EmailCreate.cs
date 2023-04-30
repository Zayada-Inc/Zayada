using Application.CommandsQueries.Email.EmailValidator;
using FluentValidation;
using Infrastructure.Interfaces;
using Infrastructure.Services.Email;
using MediatR;

namespace Application.CommandsQueries.Email
{
    public class EmailCreate
    {
        public class Command : IRequest<Unit>
        {
            public EmailRequest EmailRequest { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.EmailRequest).SetValidator(new EmailRequestValidator());
            }

            public class Handler : IRequestHandler<Command, Unit>
            {
                private readonly IEmailService _emailService;

                public Handler(IEmailService emailService)
                {
                    _emailService = emailService;
                }

                public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
                {
                    await _emailService.SendEmailAsync(request.EmailRequest.ToEmail, request.EmailRequest.Subject, request.EmailRequest.Message);
                    return Unit.Value;
                }
            }
        }
    }
}

