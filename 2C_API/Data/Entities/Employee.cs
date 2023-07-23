using System.ComponentModel.DataAnnotations;

namespace _2C_API.Data.Entities
{
    public class Employee : BaseEntity
    {
        public Employee() 
        {
                
        }

        [MaxLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string VisaType { get; set; } = string.Empty;
        public string TaskType { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime AgreementStartDate { get; set; }
        public DateTime AgreementEndDate { get; set; }
        public double YearlySalary { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

    }

}
