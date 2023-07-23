using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _2C_API.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;
        public bool IsManager { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string ChangedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

    }
}
