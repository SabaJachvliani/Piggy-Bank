using FluentValidation;
using PiggyBank.Models.DTO;

namespace PiggyBank.Validators.UserRegistrationChangePassword
{
    public class ChangePasswordValidation : AbstractValidator<ChangePassword>
    {
        public ChangePasswordValidation() 
        {
            RuleFor(x => x.Mail).NotEmpty().EmailAddress();
        }
    }
}
