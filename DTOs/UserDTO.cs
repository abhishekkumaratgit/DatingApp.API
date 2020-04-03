using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password should be between 4 to 8 characters")]
        public string password { get; set; }
    }
}