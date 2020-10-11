using System.ComponentModel.DataAnnotations;

namespace Week10Correction.DTOs
{
    public class FetchDetailsDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}