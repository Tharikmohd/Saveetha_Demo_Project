using Microsoft.EntityFrameworkCore;
using Saveetha_Demo_Project.Model;

namespace Saveetha_Demo_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Student> students { get; set; }
        public DbSet<User> users { get; set; }
    }
}
