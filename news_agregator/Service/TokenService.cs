using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Service
{
    public class TokenService
    {
        private readonly JwtOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenService(IOptions<JwtOptions> options,IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("userid", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var signingCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddHours(_options.ExpiresHours)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }

        public string GetUserIdFromToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["tasty-cookie"];

            if (token != null)
            {
                var handler  =  new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
                return userId;
            }

            return null;
        }
    }
}

