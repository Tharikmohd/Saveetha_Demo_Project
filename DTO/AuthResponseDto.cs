namespace Saveetha_Demo_Project.DTO
{
    public class AuthResponseDto
    {
        public bool success { get; set; }
        public string message { get; set; } = string.Empty;
        public int user_id { get; set; }
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

        public string token { get; set; } = string.Empty;
    }
}
