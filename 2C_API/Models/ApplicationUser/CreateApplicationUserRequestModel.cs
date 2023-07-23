using System.ComponentModel.DataAnnotations;

namespace _2C_API.Models.ApplicationUser
{
    public class CreateApplicationUserRequestModel
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }
    }
}
