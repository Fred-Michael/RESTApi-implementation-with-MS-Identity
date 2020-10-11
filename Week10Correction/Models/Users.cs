using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Week10Correction.Models
{
    public class Users : IdentityUser
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Firstname field cannot be more that 30 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Lastname field cannot be more that 30 characters")]
        public string LastName { get; set; }
        [Required]
        public string Department { get; set; }
        public DateTime DateOfEmployment { get; set; } = DateTime.Now;
        [Required]
        public string Gender { get; set; }
    }
}
