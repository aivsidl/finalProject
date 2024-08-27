using FinalProject.BusinessLayer.Models;
using FluentValidation;

namespace FinalProject.BusinessLayer.Infrastructure.Validators
{
    public class RegistrationValidator : AbstractValidator<RegisterUser>
    {

        public RegistrationValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("Your Username field is empty?");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Your Password field is empty?");
            RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Your Username is too long.");
            RuleFor(x => x.Password).MaximumLength(50).WithMessage("Your Username is too long.");
            RuleFor(x => x.UserName).MinimumLength(5).WithMessage("Your Username is too short.");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("Your Username is too short.");
            RuleFor(x => x.Password).Equal(x => x.CheckPassword).WithMessage("Your passwords do not match!");
        }
    }
}
