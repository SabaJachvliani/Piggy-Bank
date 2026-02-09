using FluentValidation;
using PiggyBank.Models.DTO;

namespace PiggyBank.Validators.UserRegistraionLogOut
{
    public class LogOutValidator : AbstractValidator<LogOut>
    {
        public LogOutValidator() 
        {
            RuleFor(x => x.Mail).NotEmpty().EmailAddress();
        }
    }
}
