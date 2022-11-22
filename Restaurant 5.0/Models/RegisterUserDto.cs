using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_5._0.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Nationality { get; set; }
        public DateTime? BirthDate { get; set; }
        public int RoleId { get; set; } = 3;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
