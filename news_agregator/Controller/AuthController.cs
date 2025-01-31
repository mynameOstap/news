using IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Controller
{
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly PasswordService _passwordService;
        private readonly IUserRepository _dbUser;
        private readonly TokenService _token;
        public AuthController(PasswordService passwordService, IUserRepository dbUser, TokenService token)
        {
            _dbUser = dbUser;
            _passwordService = passwordService;
            _token = token;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser =  await _dbUser.Get(u => u.Name == user.Name);
            if (existingUser != null)
            {
                return Conflict("Такий користувач вже існує");
            }

            var hashedpassword = _passwordService.HashPassword(user.HashPassword);
            var cur_user = new User()
            {
                Name = user.Name,
                HashPassword = hashedpassword,
                CreatedAt = DateTime.UtcNow,
                Administrator = false,
            };
            _dbUser.Create(cur_user);
            _dbUser.Save();
            return Ok(new { success = true, message = "Користувача створено" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user, HttpContext context)
        {
            var validUser = await _dbUser.Get(u => u.Name == user.Name);
            
            if (validUser == null || !_passwordService.VerifyPassword(validUser.HashPassword, user.HashPassword))
            {
                return Unauthorized(new { success = false, message = "Неправильний логін або пароль" });   
            }

            var token = _token.GenerateToken(user);
            context.Response.Cookies.Append("tasty-cookie",token);
            
            return Ok(new { success = true, message = "Ви увійшли",token});
        }
    
    }
}

