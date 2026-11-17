using Saveetha_Demo_Project.Model;

namespace Saveetha_Demo_Project.Interface
{
    public interface IUserinterface
    {
        Task<bool> usernameexistsAsync(string username);
        Task<bool> emailexitsAsync(string email);
        Task<User?> GetbyEmailAsync(string email);
        Task AddAsync(User user);
    }
}
