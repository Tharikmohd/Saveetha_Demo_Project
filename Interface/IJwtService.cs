namespace Saveetha_Demo_Project.Interface
{
    public interface IJwtService
    {
        string GenerateToken(int user_id, string username, string email);
    }
}
