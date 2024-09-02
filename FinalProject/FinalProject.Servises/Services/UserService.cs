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
                    UserAdressDto = user.UserInfo.Address == null ? null : new UserAddressDto
                    {
                        Id = user.UserInfo.Address.Id,
                        City = user.UserInfo.Address.City,
                        Street = user.UserInfo.Address.Street,
                        HouseNumber = user.UserInfo.Address.HouseNumber,
                        ApartamentNumber = user.UserInfo.Address.ApartamentNumber
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

        public async Task UpdateUserInfoDtoAsync(int id, UserInfoDto userInfoDto)
        {
            var user = await userRepository.GetUserByUserLightIdAsync(id);
            if (user == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }

            await userRepository.UpdateUserInfoAsync(id, new UserInfo
            {
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                PersonalCode = userInfoDto.PersonalCode,
                Phone = userInfoDto.Phone,
                Email = userInfoDto.Email,
            } );

        }

        public async Task UpdateUserAddressDtoAsync(int id, UserAddressDto userAddressDto)
        {
            var user = await userRepository.GetUserByUserLightIdAsync(id);
            if (user == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }

            await userRepository.UpdateUserAddressAsync(id, new Address
            {
                
                City = userAddressDto.City,
                Street = userAddressDto.Street,
                HouseNumber = userAddressDto.HouseNumber,
                ApartamentNumber = userAddressDto.ApartamentNumber,
               
            });
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteUserAsync(id);
        }

        public async Task AddUserInfoDtoAsync(int id, UserInfoDto userInfoDto)
        {
            var user = await userRepository.GetUserByUserLightIdAsync(id);
            if (user == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }

            await userRepository.AddUserInfoAsync(id, new UserInfo
            {
                FirstName = userInfoDto.FirstName,
                LastName = userInfoDto.LastName,
                PersonalCode = userInfoDto.PersonalCode,
                Phone = userInfoDto.Phone,
                Email = userInfoDto.Email,
            });
        }

        public async Task AddUserAddressDtoAsync(int id, UserAddressDto userAddressDto)
        {
            var user = await userRepository.GetUserByUserLightIdAsync(id);
            if (user == null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, $"User with {id} not found");
            }

            await userRepository.AddUserAddressAsync(id, new Address
            {

                City = userAddressDto.City,
                Street = userAddressDto.Street,
                HouseNumber = userAddressDto.HouseNumber,
                ApartamentNumber = userAddressDto.ApartamentNumber,

            });
        }
    }
}
