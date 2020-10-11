using System.ComponentModel.DataAnnotations;

namespace Week10Correction.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Firstname field cannot be more that 30 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Lastname field cannot be more that 30 characters")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}