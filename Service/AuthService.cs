using Microsoft.AspNetCore.Identity;
using Saveetha_Demo_Project.DTO;
using Saveetha_Demo_Project.Interface;
using Saveetha_Demo_Project.Model;

namespace Saveetha_Demo_Project.Service
{
    public class AuthService : IAuthInterface
    {
        private readonly IUserinterface userrepo;
        private readonly IPasswordHasher<User> pwdhsh;
        private readonly IJwtService jwtService;

        public AuthService(IUserinterface usersrepo, IPasswordHasher<User> hasher, IJwtService jwt)
        {
            userrepo = usersrepo;
            pwdhsh = hasher;
            jwtService = jwt;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await userrepo.GetbyEmailAsync(dto.email);
            if(user == null)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Invalid email and password"
                };
            }

            var pwd = pwdhsh.VerifyHashedPassword(user, user.password, dto.password);
            if(pwd == PasswordVerificationResult.Failed)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Invalid email or password"
                };
            }

            var token = jwtService.GenerateToken(
                user.id,
                user.username,
                user.email);

            return new AuthResponseDto
            {
                success = true,
                message = "Welcome back",
                user_id = user.id,
                username = user.username,
                email = user.email,
                token = token

            };
        }

        public async Task<AuthResponseDto> RegistrationAsync(RegisterDto dto)
        {
            bool usernamexits = await userrepo.usernameexistsAsync(dto.username);
            if(usernamexits)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "Username already exists"
                };
            }

            bool emailexists = await userrepo.emailexitsAsync(dto.username);
            if (emailexists)
            {
                return new AuthResponseDto
                {
                    success = false,
                    message = "email already registered"
                };
            }

            var user = new User
            {
                username = dto.username,
                email = dto.email,
                fullname = dto.fullname
            };

            user.password = pwdhsh.HashPassword(user, dto.password);

            await userrepo.AddAsync(user);

            return new AuthResponseDto
            {
                success = true,
                message = "Account successfully Registered",
                user_id = user.id,
                username = user.username,
                email = user.email
            };
        }
    }
}
