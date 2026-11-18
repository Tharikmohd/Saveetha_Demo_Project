using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saveetha_Demo_Project.Data;
using Saveetha_Demo_Project.DTO;
using Saveetha_Demo_Project.Interface;
using Saveetha_Demo_Project.Model;

namespace Saveetha_Demo_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface authrepo;

        public AuthController(IAuthInterface auth)
        {
            authrepo = auth;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await authrepo.RegistrationAsync(dto);
            if (!result.success)
                return Conflict(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await authrepo.LoginAsync(dto);
            if (!result.success)
                return Unauthorized(result);

            return Ok(result);

        }
    }
}
