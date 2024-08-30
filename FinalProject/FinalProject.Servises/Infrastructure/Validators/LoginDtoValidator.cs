using FinalProject.BusinessLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Infrastructure.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Your Username field is empty?");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Your Password field is empty?");        
            
        }
    }
}
