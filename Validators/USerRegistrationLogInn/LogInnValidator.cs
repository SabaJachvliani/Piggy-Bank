using FluentValidation;
using PiggyBank.Models.DTO;

namespace PiggyBank.Validators.USerRegistrationLogInn
{
    public class LogInnValidator : AbstractValidator<LogInn>
    {
        public LogInnValidator() 
        { 
            RuleFor(x => x.Mail).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .Matches(@"\d").WithMessage("Password must contain at least 1 number")
            .Matches(@"[^\w\s]").WithMessage("Password must contain at least 1 symbol");
        }
        
    }
}
