using System.ComponentModel.DataAnnotations;

namespace Week10Correction.DTOs
{
    public class UserToLoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
