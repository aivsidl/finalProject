using FinalProject.BusinessLayer.Infrastructure.Handlers;
using FinalProject.BusinessLayer.Infrastructure.Handlers.Interfaces;
using FinalProject.BusinessLayer.Infrastructure.Validators;
using FinalProject.BusinessLayer.Models;
using FinalProject.BusinessLayer.Services.Interfaces;
using FinalProject.DataLayer.Models;
using FinalProject.DataLayer.Repositories.Interfaces;
using FluentValidation;
using FinalProject.BusinessLayer.Infrastructure.Exeptions;
using System.Net;


namespace FinalProject.BusinessLayer.Services
{
    public class UserService : IUserService
    {
      
        private readonly IJwtTokenhandler jwtTokenHandler;
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository, IJwtTokenhandler jwtTokenHandler)
        {
           this.userRepository = userRepository;
           this.jwtTokenHandler = jwtTokenHandler;           

        }

        public async Task AddAsync(RegisterUser registerUser)
            
        {
            //patikrinti ar egzistuoja toks useris

            var validator = new RegistrationValidator();
            var validationResult = await validator.ValidateAsync(registerUser);

            if (!validationResult.IsValid) 
            {
               await validator.ValidateAndThrowAsync(registerUser);                    
            }

            var salt = Salt.Create();
            var password = Hash.Create(registerUser.Password, salt);
            await userRepository.AddAsync(new User { UserName = registerUser.UserName, Password = password , Salt = salt }) ;           
        }
        
        
        // to do: ------------------------------------- map  from user to userDto

        public async Task<User> GetUserByIdAsync(int id)
        { 
            var a = await userRepository.GetUserByUsernameIdAsync(id);
            if (a == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }
            return a;

            
        }

        public async Task<string> LoginAsync(UserDto userDto)
        {
            var validator = new UserDtoValidator();
            var validationResult = await validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                await validator.ValidateAndThrowAsync(userDto);
            }

           var user = await userRepository.GetUserByUsernameAsync(userDto.UserName);
            if (user == null || !Validation.Validate(userDto.Password, user.Salt, user.Password))
            {
                throw new Exception();
            }

            var token = await this.jwtTokenHandler.CreateJWTTokenAsync(userDto.UserName);
            return token;


        }

    }
}
