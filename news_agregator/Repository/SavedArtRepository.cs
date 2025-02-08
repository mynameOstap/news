
using Data;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class SavedArtRepository : Repository<SavedArticle>, ISavedArtRepository
    {
        private readonly Context _db;

        public SavedArtRepository(Context db) : base(db)
        {
            _db = db;
        }

        public async Task<List<SavedArticleResponse>> GetAllArticle(int userid)
        {
            var savedArticles = await _db.SavedArticles
                .Where(s => s.UserId == userid)
                .Include(s => s.NewsArticle) // Завантажуємо пов'язані статті
                .Select(s => new SavedArticleResponse
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    ArticleId = s.ArticleId,
                    SavedAt = s.SavedAt,
                    ArticleTitle = s.NewsArticle.Title,
                    ArticleAuthor = s.NewsArticle.Author,
                    ArticleUrl = s.NewsArticle.Url
                })
                .ToListAsync();

            if (savedArticles == null)
            {
                return new List<SavedArticleResponse>(); 
            }

            return savedArticles;
        }
        
    }
}

