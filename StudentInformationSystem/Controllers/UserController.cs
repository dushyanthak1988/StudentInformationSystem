using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentInformationSystem.Models;
using StudentInformationSystem.Models.RequestModel;
using StudentInformationSystem.Repository;

namespace StudentInformationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;


        public UserController(
                                UserManager<ApplicationUser> userManager,
                                IJwtService jwtService
                                )
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0

            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Registration successful" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = _jwtService.GenerateTokens(user);

                return Ok(new
                {
                    token.AccessToken,
                    expiration = token.ExpiresDate,
                    token.RefreshToken

                });
            }

            return Unauthorized();
        }


    }
}