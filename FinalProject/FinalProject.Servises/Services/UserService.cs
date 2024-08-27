using FinalProject.BusinessLayer.Infrastructure.Validators;
using FinalProject.BusinessLayer.Models;
using FinalProject.BusinessLayer.Services.Interfaces;
using FinalProject.DataLayer.Models;
using FinalProject.DataLayer.Repositories.Interfaces;
using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
           _userRepository = userRepository;
        }
        public async Task AddAsync(RegisterUser registerUser)
        {
            var validator = new RegistrationValidator();
            var validationResult = await validator.ValidateAsync(registerUser);

            if (!validationResult.IsValid) 
            {
               await validator.ValidateAndThrowAsync(registerUser);
                    
            }

            await _userRepository.AddAsync(new User { UserName = registerUser.UserName, Password = registerUser.Password  }) ;           
        }
    }
}
