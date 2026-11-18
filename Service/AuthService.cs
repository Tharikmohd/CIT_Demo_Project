using CIt_Demo_Project.DTO;
using CIt_Demo_Project.Interface;
using CIt_Demo_Project.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace CIt_Demo_Project.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userrepo;
        private readonly IPasswordHasher<User> pwdhasher;
        private readonly IJwtService jwtService;


        public AuthService(IUserRepository usersrepo,IPasswordHasher<User> pwdhsh, IJwtService jwt)
        {
            userrepo = usersrepo;
            pwdhasher = pwdhsh;
            jwtService = jwt;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var result = await userrepo.GetbyEmailAsync(dto.email);
            if(result == null)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Invalid username or Password"
                };
            }

            var pwd = pwdhasher.VerifyHashedPassword(result, result.password, dto.password);
            if (pwd == PasswordVerificationResult.Failed)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Invalid username or Password"
                };
            }
            var token = jwtService.GenerateToken(
                result.id,
                result.username,
                result.email
            );

            return new AuthResponseDto
            {
                success = true,
                message = "Welcome back",
                user_id = result.id,
                username = result.username,
                email = result.email,
                token = token

            };

        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            bool usernamexits = await userrepo.usernameexistsAsync(dto.username);
            if (usernamexits)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Username already exists"
                };
            }

            bool emailexists = await userrepo.emailexistsAsync(dto.email);
            if (emailexists)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Email already exists"
                };
            }

            var user = new User
            {
                username = dto.username,
                fullname = dto.fullname,
                email = dto.email
            };
            user.password = pwdhasher.HashPassword(user, dto.password);

            await userrepo.AddAsync(user);

            return new AuthResponseDto
            {
                success = true,
                message = "Registration successful",
                user_id = user.id,
                username = user.username,
                email = user.email
            };
        }
    }
}
