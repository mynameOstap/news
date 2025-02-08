

using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Controller
{
    [ApiController]
    [Route("api/")]
    public class SavedArtController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IRepository<SavedArticle> _dbSave;
        private readonly ISavedArtRepository _db;

        public SavedArtController(IRepository<SavedArticle> dbSave, TokenService tokenService, ISavedArtRepository db)
        {
            _dbSave = dbSave;
            _tokenService = tokenService;
            _db = db;
        }
        [Authorize]
        [HttpPost("/savearticle")]
        public async Task<IActionResult> SaveArticle([FromQuery] int id)
        {
            var userid = _tokenService.GetUserIdFromToken();
            var useridInt = int.Parse(userid);
            var saveArt = new SavedArticle()
            {
                UserId = useridInt,
                ArticleId = id,
                SavedAt = DateTime.UtcNow
            };
             _dbSave.Create(saveArt);
            await _dbSave.Save();
            return Ok(new { succes = true, massege = "Успішно збережено", saveArt });
        }

        [Authorize]
        [HttpDelete("/deletearticle")]
        public async Task<IActionResult> DeleteArticle([FromQuery] int id)
        {
            var userid = _tokenService.GetUserIdFromToken();
            var useridInt = int.Parse(userid);
            
            var deleteAricle = await _dbSave.Get(a => a.ArticleId == id);
            if (deleteAricle != null  && deleteAricle.UserId == useridInt )
            {
                _dbSave.Remove(deleteAricle);
                await _dbSave.Save();

                return Ok(new
                {
                    succes = true,
                    massege = "Успішно видалено",
                    deleteAricle
                });
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("/getarticle")]  // Виправлений маршрут
        public async Task<IActionResult> GetAllArticle()
        {
            // Отримуємо користувача з токена
            var userid = _tokenService.GetUserIdFromToken();
            if (string.IsNullOrEmpty(userid))
            {
                return NotFound("Не вдалося отримати ID користувача через токен.");
            }

            // Безпечний парсинг ID користувача
            if (!int.TryParse(userid, out int useridInt))
            {
                return BadRequest("Невірний формат ID користувача.");
            }

            // Отримуємо збережені статті
            var result = await _db.GetAllArticle(useridInt);

            // Перевірка на null або порожній список
            if (result == null || result.Count == 0)
            {
                return NotFound("Не знайдено збережених статей.");
            }

            // Повертаємо результат
            return Ok(result);
        }

        
    }
}

