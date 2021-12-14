using DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Modules.Jwt
{
    public class JwtService
    {
        private readonly JwtConfig jwtConfig;

        public JwtService(IOptionsMonitor<JwtConfig> config)
        {
            jwtConfig = config.CurrentValue;
        }

        public string GenerateAccessToken(Customer customer, Guid session)
        {
            JwtSecurityTokenHandler jwtHandler = new();

            byte[] key = Encoding.UTF8.GetBytes(jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                   new Claim("Id", customer.Id.ToString()),
                   new Claim("sessionId", session.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub,customer.Username),
                    new Claim(JwtRegisteredClaimNames.UniqueName, customer.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                }),
                Expires = DateTime.Now.AddDays(1),
                Issuer = "issuer",
                Audience = "issuer",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            SecurityToken token = jwtHandler.CreateToken(tokenDescriptor);

            return jwtHandler.WriteToken(token);
        }
    }
}
