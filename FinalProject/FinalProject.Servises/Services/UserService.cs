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
        
        
        public async Task<UserDto> GetUserByIdAsync(int id)
        { 

            var user = await userRepository.GetUserByUsernameIdAsync(id);
            if (user == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }
            return new UserDto { 
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
                UserInfoDto = user.UserInfo == null ? null : new UserInfoDto
                {
                    Id = user.UserInfo.Id,
                    FirstName = user.UserInfo.FirstName,
                    LastName = user.UserInfo.LastName,
                    PersonalCode = user.UserInfo.PersonalCode,
                    Phone = user.UserInfo.Phone,
                    Email = user.UserInfo.Email,
                    UserAdressDto = user.UserInfo.Adress == null ? null : new UserAdressDto
                    {
                        Id = user.UserInfo.Adress.Id,
                        City = user.UserInfo.Adress.City,
                        Street = user.UserInfo.Adress.Street,
                        HouseNumber = user.UserInfo.Adress.HouseNumber,
                        ApartamentNumber = user.UserInfo.Adress.ApartamentNumber
                    }


                }
               
            };

            
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var validator = new LoginDtoValidator();
            var validationResult = await validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                await validator.ValidateAndThrowAsync(loginDto);
            }

           var user = await userRepository.GetUserByUsernameAsync(loginDto.UserName);
            if (user == null || !Validation.Validate(loginDto.Password, user.Salt, user.Password))
            {
                throw new Exception();
            }

            var token = await this.jwtTokenHandler.CreateJWTTokenAsync(loginDto.UserName);
            return token;

        }

        public async Task<int> UpdateUserInfoDtoAsync(int id, UserInfoDto userInfoDto)
        {
            var user = await userRepository.GetUserByUserLightIdAsync(id);
            if (user == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }

            var result = await userRepository.UpdateUserInfoAsync(id, new UserInfo
            {
                Id = userInfoDto.Id,
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                PersonalCode = userInfoDto.PersonalCode,
                Phone = userInfoDto.Phone,
                Email = userInfoDto.Email,
            } );

            return result;
        }
    }
}
