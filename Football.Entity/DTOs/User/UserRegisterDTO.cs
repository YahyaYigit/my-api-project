

using System.ComponentModel.DataAnnotations;

namespace Basketball.Entity.DTOs.User
{
    public class UserRegisterDTO
    {
        [Required]
        public string FirstName { get; set; } 

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public DateTime BirthDay { get; set; }

        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MotherName { get; set; }
        public string? MotherPhoneNumber { get; set; }
        public string? FatherName { get; set; }
        public string? FatherPhoneNumber { get; set; }
    }
}
