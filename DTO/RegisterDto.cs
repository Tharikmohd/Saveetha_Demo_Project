using System.ComponentModel.DataAnnotations;

namespace Saveetha_Demo_Project.DTO
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public string username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string fullname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [MaxLength(100)]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be atleast 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be contains 1 uppercase, 1 lowercase and 1 special characters")]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm Password")]
        [Compare("password", ErrorMessage = "Password doesn't match")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; } = string.Empty;



    }
}
