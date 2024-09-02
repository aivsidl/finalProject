using FinalProject.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DataLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Adresses { get; set; }

        public DbSet<UserInfo> UsersInfo { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

                   
        
    }
}
