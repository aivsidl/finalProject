using FinalProject.DataLayer.Data;
using FinalProject.DataLayer.Models;
using FinalProject.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;

        }

        public async Task AddAsync(User user)
        {
            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await this.context.Users.SingleOrDefaultAsync(u=>u.UserName == username);

        }

        public async Task<User> GetUserByUsernameIdAsync(int id)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
