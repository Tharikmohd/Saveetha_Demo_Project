using Microsoft.EntityFrameworkCore;
using Saveetha_Demo_Project.Data;
using Saveetha_Demo_Project.Interface;
using Saveetha_Demo_Project.Model;

namespace Saveetha_Demo_Project.Repository
{
    public class UserRepository : IUserinterface
    {
        private readonly AppDbContext appDb;
        public UserRepository(AppDbContext dbContext)
        {
            appDb = dbContext;
        }
        public async Task AddAsync(User user)
        {
             await appDb.users.AddAsync(user);
            await appDb.SaveChangesAsync();
        }

        public async Task<bool> emailexitsAsync(string email)
        {
            return await appDb.users.AnyAsync(u => u.email == email);
        }

        public async Task<User?> GetbyEmailAsync(string email)
        {
            return await appDb.users.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<bool> usernameexistsAsync(string username)
        {
            return await appDb.users.AnyAsync(u => u.username == username);
        }
    }
}
