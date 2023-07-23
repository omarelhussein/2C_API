using _2C_API.Data;
using _2C_API.Data.DTOs;
using _2C_API.Data.Entities;
using _2C_API.Models.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2C_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _context;
        public ApplicationUserController(DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateApplicationUser([FromBody] CreateApplicationUserRequestModel request)
        {
            bool isUserExists = await _context.ApplicationUsers.Where(x => !x.IsDeleted && x.Email == request.Email).AnyAsync();
            if (isUserExists)
            {
                return Forbid("User is already exists.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsManager = request.IsManager,
                PhoneNumber = request.PhoneNumber,
                UserName = "TwoCApplicationUser."
            };

            var createUser = await _userManager.CreateAsync(user, request.Password);
            if (createUser.Succeeded)
            {
                return Ok(new
                {
                    Result = "Succeeded",
                    UserId = user.Id
                }); 
            }

            return Forbid("Error while creating the user.");

        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<ApplicationUserViewModel>>> GetAllUsers()
        {
            var allUsers = await _context.ApplicationUsers.Where(x => !x.IsDeleted)
                                                           .Select(x=> new ApplicationUserViewModel
                                                           {
                                                               Id = x.Id,
                                                               Email = x.Email,
                                                               FirstName = x.FirstName,
                                                               LastName = x.LastName,
                                                               PhoneNumber = x.PhoneNumber,
                                                               IsManager = x.IsManager,

                                                               CreatedAt = x.CreatedAt,
                                                               CreatedBy = x.CreatedBy
                                                           })
                                                           .ToListAsync();

            return Ok(allUsers);
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<ApplicationUserViewModel>> GetUserById(string Id)
        {
            var user = await _context.ApplicationUsers.Where(x => !x.IsDeleted && x.Id == Id)
                                                           .Select(x => new ApplicationUserViewModel
                                                           {
                                                               Id = x.Id,
                                                               Email = x.Email,
                                                               FirstName = x.FirstName,
                                                               LastName = x.LastName,
                                                               PhoneNumber = x.PhoneNumber,
                                                               IsManager = x.IsManager,

                                                               CreatedAt = x.CreatedAt,
                                                               CreatedBy = x.CreatedBy
                                                           })
                                                           .SingleOrDefaultAsync();
            if(user != null)
            {
                return Ok(user);
            }

            return NotFound("User not found.");
        }

    }
}
