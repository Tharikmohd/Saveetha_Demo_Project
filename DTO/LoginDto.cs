using System.ComponentModel.DataAnnotations;

namespace Saveetha_Demo_Project.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100)]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(100)]
        public string password { get; set; } = string.Empty;
    }
}
