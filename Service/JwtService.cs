using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Saveetha_Demo_Project.Interface;
using System.Text;

namespace Saveetha_Demo_Project.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration config;

        public JwtService(IConfiguration configuration)
        {
            config = configuration;
        }
        public string GenerateToken(int user_id, string username, string email)
        {
            var key = config["Jwt:Key"];
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];

            if(string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer) 
                                        || string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException("Jwt Configuration is missing");
            }

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credential = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user_id.ToString()),
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes
                (Convert.ToDouble(config["Jwt:Expires"] ?? "30")),
                signingCredentials : credential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
