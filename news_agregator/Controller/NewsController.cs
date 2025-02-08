using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using NewsAPI.Models;
using Service;

namespace Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IRepository<NewsArticle> _dbNews;
        private readonly NewsService _news;
        private readonly TokenService _tokenService;

        public NewsController(IRepository<NewsArticle> dbNews, NewsService news, TokenService tokenService,IRepository<Comment> dbComment)
        {
            _dbNews = dbNews;
            _news = news;
            _tokenService = tokenService;
            
        }

        [HttpGet("news")]
        [Authorize]
        public async Task<IActionResult> GetNews([FromQuery] string query, string category = null,
            string language = "uk")
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("The 'query' parameter is required.");
            }

            var articles = await _news.GetNewsAsync(query, category, language);
            Console.WriteLine(articles);
            
            return Ok(articles);
        }

       
        
    }
}

