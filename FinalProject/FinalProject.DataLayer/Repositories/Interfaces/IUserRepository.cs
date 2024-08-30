using FinalProject.DataLayer.Models;

namespace FinalProject.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task AddAsync(User user);

        public Task<User> GetUserByUsernameAsync(string username);

        public Task<User> GetUserByUsernameIdAsync(int id);

        public Task<User> GetUserByUserLightIdAsync(int id);

        public Task<int> UpdateUserInfoAsync (int id, UserInfo userInfo);


    }
}
