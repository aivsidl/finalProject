using FinalProject.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.API.Database
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options) { }

        public DbSet<User> Users  { get; set; }
    }
}
