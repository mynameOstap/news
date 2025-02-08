

using Model;

namespace IRepository
{
    public interface ISavedArtRepository : IRepository<SavedArticle>
    {
        Task<List<SavedArticleResponse>> GetAllArticle(int userid);
    }
}

