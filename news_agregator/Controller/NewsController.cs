using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace Controller
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IRepository<NewsArticle> _dbNews;
        private readonly NewsService _news;

        public NewsController(IRepository<NewsArticle> dbNews,NewsService news)
        {
            _dbNews = dbNews;
            _news = news;
        }

        [HttpGet("news")]
        [Authorize]
        public async Task<List<sources>> GetNews([FromQuery] string category = null)
        {
            return await _news.GetNewsAsync(category);
        }
    }
}

