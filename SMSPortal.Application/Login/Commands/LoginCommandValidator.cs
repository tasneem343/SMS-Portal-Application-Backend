

using FluentValidation;

namespace SMSPortal.Application.Auth.Login.Commands
{
    public class LogoutCommandValidator : AbstractValidator<LoginCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty();
        
        }
    }
}
