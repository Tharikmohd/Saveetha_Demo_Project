using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Saveetha_Demo_Project.DTO;


namespace Saveetha_Demo_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public IActionResult GetProfile()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new 
            {
                message = "Jwt Authentication successful",
                userid = userid,
                email = email,
                username = username

            });


        }
    }
}
