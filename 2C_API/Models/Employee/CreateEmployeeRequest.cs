using System.ComponentModel.DataAnnotations;

namespace _2C_API.Models.Employee
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string VisaType { get; set; } 
        public string TaskType { get; set; } 
        public string Nationality { get; set; } 
        public DateTime AgreementStartDate { get; set; }
        public DateTime AgreementEndDate { get; set; }
        public double YearlySalary { get; set; }
        public string Currency { get; set; } 
        public string? Notes { get; set; } 

    }
}
