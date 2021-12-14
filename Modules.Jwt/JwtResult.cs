using DAL.Entities;

namespace Modules.Jwt
{
    public class JwtResult
    {
        public string Token { get; set; }
        public Session RefreshToken { get; set; }
        public bool Success { get; set; }
        public string RefreshTokenValue { get; set; }
    }
}
