using FinalProject.DataLayer.Models;

namespace FinalProject.DataLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task AddAsync(User user);

        public Task<User> GetUserByUsernameAsync(string username);
    }
}
