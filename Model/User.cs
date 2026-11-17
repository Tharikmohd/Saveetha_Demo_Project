using System.ComponentModel.DataAnnotations;

namespace Saveetha_Demo_Project.Model
{
    public class User
    {
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string fullname { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string password { get; set; } = string.Empty;


    }
}
