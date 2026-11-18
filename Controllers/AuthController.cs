using CIt_Demo_Project.Data;
using CIt_Demo_Project.DTO;
using CIt_Demo_Project.Interface;
using CIt_Demo_Project.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CIt_Demo_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService auth;

        public AuthController(IAuthService authService)
        {
            auth = authService;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterDto dto)
        {
            var result = await auth.RegisterAsync(dto);
            if (!result.success)
                return Conflict(result);

            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await auth.LoginAsync(dto);
            if (!result.success)
                return Conflict(result);

            return Ok(result);
            
        }

    }
}
