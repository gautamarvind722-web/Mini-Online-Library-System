using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Backed.Models;
using Mini_Backed.Services.Interface;

namespace Mini_Backed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var created = await _userService.Register(user);
            if (created == null) return BadRequest("Email already exists");
            return Ok(created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _userService.GenerateJwt(user);

            // Update: use UserId instead of Id
            return Ok(new { token, userId = user.UserId, role = user.Role });
        }
    }
}
