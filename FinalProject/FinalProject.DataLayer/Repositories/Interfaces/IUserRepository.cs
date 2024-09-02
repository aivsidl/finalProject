using FinalProject.DataLayer.Models;

namespace FinalProject.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task AddAsync(User user);

        public Task<User> GetUserByUsernameAsync(string username);

        public Task<User> GetUserByUsernameIdAsync(int id);

        public Task<User> GetUserByUserLightIdAsync(int id);

        public Task UpdateUserInfoAsync (int id, UserInfo userInfo);

        public Task UpdateUserAddressAsync(int id, Address address);

        public Task DeleteUserAsync(int id);

        public Task AddUserInfoAsync(int id, UserInfo userInfo);

        public Task AddUserAddressAsync(int id, Address address);


    }
}
