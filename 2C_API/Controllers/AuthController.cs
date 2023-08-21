using _2C_API.Data;
using _2C_API.Data.Entities;
using _2C_API.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2C_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(DatabaseContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request) 
        {
            var user = _context.ApplicationUsers.Where(x => x.Email == request.email && !x.IsDeleted).SingleOrDefault();
            if (user == null)
            {
                return BadRequest("User doesn't exist.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.password, false);
            if (result.Succeeded)
            {
                var userId = user.Id;
                Response.Cookies.Append("LoginCookie", userId);

                return Ok("Successful login.");
            }
            else
            {
                return Forbid("Wrong password");
            }
        }
    }
}
