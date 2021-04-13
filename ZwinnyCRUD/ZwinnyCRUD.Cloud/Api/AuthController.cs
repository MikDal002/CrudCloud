using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ZwinnyCRUD.Cloud.Api
{
    public class UserDto
    {        
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserDto user)
        {
            var myUser = new IdentityUser { Email = user.Mail, UserName = user.Mail };

            var userCreateResult = await _userManager.CreateAsync(myUser, user.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserDto user)
        {
            var result = await _signInManager.PasswordSignInAsync(
                    user.Mail, user.Password, true, false);

            if (result.Succeeded)
            {
                return Ok(user.Mail);
            }

            return BadRequest("Email or password incorrect.");
        }
    }
}
