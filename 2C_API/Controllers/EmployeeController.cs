using _2C_API.Data;
using _2C_API.Data.DTOs;
using _2C_API.Data.Entities;
using _2C_API.Models.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2C_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public EmployeeController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<EmployeeViewModel>>> GetAllEmployees()
        {
            var employees = await _context.Employees.Where(x => !x.IsDeleted).ToListAsync();

            var response = employees.Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                AgreementStartDate = x.AgreementStartDate,
                AgreementEndDate = x.AgreementEndDate,
                Nationality = x.Nationality,
                TaskType = x.TaskType,
                VisaType = x.VisaType,
                YearlySalary = x.YearlySalary,
                Currency = x.Currency,
                Notes = x.Notes,

            }).ToList();

            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync(); 
            if(employee != null)
            {
                var response = new EmployeeViewModel
                {
                    Id = employee.Id,
                    Email = employee.Email,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    PhoneNumber = employee.PhoneNumber,
                    AgreementStartDate = employee.AgreementStartDate,
                    AgreementEndDate = employee.AgreementEndDate,
                    Nationality = employee.Nationality,
                    TaskType = employee.TaskType,
                    VisaType = employee.VisaType,
                    YearlySalary = employee.YearlySalary,
                    Currency = employee.Currency,
                    Notes = employee.Notes
                };

                return Ok(response);
            }

            return NotFound("Employee is not found.");
        }

        [HttpPost("create")]
        public async Task<ActionResult<EmployeeViewModel>> CreateEmployee(CreateEmployeeRequest request)
        {
            var entity = new Employee
            {
                Email = request.Email,
                FirstName= request.FirstName,
                LastName= request.LastName,
                AgreementEndDate= request.AgreementEndDate,
                Nationality = request.Nationality,
                TaskType = request.TaskType,
                AgreementStartDate= request.AgreementStartDate,
                Currency = request.Currency ,
                Notes = request.Notes,
                PhoneNumber = request.PhoneNumber,
                VisaType = request.VisaType,
                YearlySalary = request.YearlySalary,
                
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "admin",
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new EmployeeViewModel
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                AgreementStartDate = entity.AgreementStartDate,
                AgreementEndDate = entity.AgreementEndDate,
                Nationality = entity.Nationality,
                TaskType = entity.TaskType,
                VisaType = entity.VisaType,
                YearlySalary = entity.YearlySalary,
                Currency = entity.Currency,
                Notes = entity.Notes
            });

        }



    }
}
