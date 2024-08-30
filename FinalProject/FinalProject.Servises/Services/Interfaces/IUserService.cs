using FinalProject.BusinessLayer.Models;
using FinalProject.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddAsync(RegisterUser registerUser);

        public Task<string> LoginAsync(LoginDto loginDto);

        public Task<UserDto> GetUserByIdAsync(int id);

        public Task<int> UpdateUserInfoDtoAsync(int id, UserInfoDto userInfoDto);

    }
}
