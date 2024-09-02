using FinalProject.DataLayer.Data;
using FinalProject.DataLayer.Models;
using FinalProject.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        public async Task<User> GetUserByUserLightIdAsync(int id)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.UserName == username);

        }

        public async Task<User> GetUserByUsernameIdAsync(int id)
        {
            return await this.context.Users.Include(x => x.UserInfo)
                .ThenInclude(x => x.Address)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserInfoAsync(int id, UserInfo userInfo)
        {
            var user = await this.context.Users.Include(x=>x.UserInfo).SingleOrDefaultAsync(u => u.Id == id);
            if (user?.UserInfo == null)
            {
                throw new Exception("UserInfo not found");
            }
            user.UserInfo.FirstName = userInfo.FirstName;
            user.UserInfo.LastName = userInfo.LastName;
            user.UserInfo.PersonalCode = userInfo.PersonalCode;
            user.UserInfo.Email = userInfo.Email;
            user.UserInfo.Phone = userInfo.Phone;

            await this.context.SaveChangesAsync();
           
        }


        public async Task UpdateUserAddressAsync(int id, Address address)
        {

            var user = await this.context.Users.Include(x=>x.UserInfo).ThenInclude(x => x.Address).SingleOrDefaultAsync(user => user.Id == id);
            if (user?.UserInfo?.Address == null)
            {
                throw new Exception ("UserAddress not found");
            }

            user.UserInfo.Address.City = address.City;
            user.UserInfo.Address.Street = address.Street;
            user.UserInfo.Address.HouseNumber = address.HouseNumber;
            user.UserInfo.Address.ApartamentNumber = address.ApartamentNumber;

            await this.context.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await this.context.Users
                .Include(u => u.UserInfo)
                .ThenInclude(ui => ui.Address)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            this.context.Users.Remove(user);

            await this.context.SaveChangesAsync();
        }

        public async Task AddUserInfoAsync(int id, UserInfo userInfo)
        {

           var user = await this.context.Users.SingleOrDefaultAsync(user => user.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.UserInfo = userInfo;
            await this.context.SaveChangesAsync();

        }

        public async Task AddUserAddressAsync(int id, Address address)
        {
            var user = await this.context.Users.Include(x=>x.UserInfo).SingleOrDefaultAsync(user => user.Id == id);
            if (user?.UserInfo == null)
            {
                throw new Exception("UserInfo not found");
            }
            user.UserInfo.Address = address;
            await this.context.SaveChangesAsync();
        }
    }
}
