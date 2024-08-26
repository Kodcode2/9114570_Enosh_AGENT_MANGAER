using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace agent_api.Service
{
    public class JwtService(IConfiguration configuration) : IJwtService
    {
        public string GenerateToken(string serverName)
        {
            string key = configuration.GetValue("Jwt:Key", "")
                ?? throw new ArgumentNullException(nameof(configuration));
            int expiry = configuration.GetValue("Jwt:ExpiryInMinutes", 60);

            var secuirtyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secuirtyKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, serverName)              
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(expiry),
                claims: claims,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
