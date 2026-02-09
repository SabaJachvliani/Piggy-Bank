using FluentValidation;
using PiggyBank.Models.DTO;

namespace PiggyBank.Validators.UserRegistration
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .Matches(@"\d").WithMessage("Password must contain at least 1 number")
            .Matches(@"[^\w\s]").WithMessage("Password must contain at least 1 symbol");
        }
    }
}
