using FinalProject.DataLayer.Data;
using FinalProject.DataLayer.Models;
using FinalProject.DataLayer.Repositories.Interfaces;

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
        
    }
}
