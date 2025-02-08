using IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Controller
{
    [ApiController]
    [Route("[controller]")]
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
            try 
            {
                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.HashPassword))
                {
                    return BadRequest(new { success = false, message = "Ім'я та пароль обов'язкові" });
                }

                var existingUser = await _dbUser.Get(u => u.Name == user.Name);
                if (existingUser != null)
                {
                    return Conflict(new { success = false, message = "Такий користувач вже існує" });
                }

                var hashedpassword = _passwordService.HashPassword(user.HashPassword);
                var newUser = new User
                {
                    Name = user.Name,
                    HashPassword = hashedpassword,
                    CreatedAt = DateTime.UtcNow,
                    Administrator = false,
                    Comments = new List<Comment>(),
                    SavedArticles = new List<SavedArticle>(),
                    Notifications = new List<Notification>()
                };

                 _dbUser.Create(newUser);
                await _dbUser.Save();
                
                return Ok(new { success = true, message = "Користувача створено" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Помилка при створенні користувача", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var validUser = await _dbUser.Get(u => u.Name == user.Name);
            
            if (validUser == null || !_passwordService.VerifyPassword(validUser.HashPassword, user.HashPassword))
            {
                return Unauthorized(new { success = false, message = "Неправильний логін або пароль" });   
            }

            var token = _token.GenerateToken(validUser);
            Response.Cookies.Append("tasty-cookie", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            
            return Ok(new { success = true, message = "Ви увійшли", token });
        }
    
    }
}

