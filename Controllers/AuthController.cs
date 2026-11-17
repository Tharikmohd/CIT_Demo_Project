using CIt_Demo_Project.Data;
using CIt_Demo_Project.DTO;
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
        private readonly AppDbContext dbContext;
        private readonly PasswordHasher<User> pwdhsh;

        public AuthController(AppDbContext appDb)
        {
            dbContext = appDb;
            pwdhsh = new PasswordHasher<User>();
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool usernameexist = await dbContext.users.AnyAsync(u => u.username == dto.username);
            if (usernameexist)
                return Conflict("Username already exits");

            bool emailexists = await dbContext.users.AnyAsync(u => u.email == dto.email);
            if (emailexists)
                return Conflict("Email already Registered");

            var user = new User
            {
                username = dto.username,
                fullname = dto.fullname,
                email = dto.email
            };

            user.password = pwdhsh.HashPassword(user, dto.password);

            dbContext.users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Registration succesfuly completed", user_id = user.id });
        }

    }
}
