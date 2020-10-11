using System;

namespace Week10Correction.DTOs
{
    public class LoggedInUserDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public DateTime DateEmployed { get; set; }
    }
}