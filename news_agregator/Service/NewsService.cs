


using System.Text.Json;
using System.Web;
using Model;

namespace Service
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "85fc41331ba540da80351fb4e949e501";
        private const string BaseUrl = "https://newsapi.org/v2/top-headlines?";

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<sources>> GetNewsAsync(string category = null)
        {
            var allAricles =  new List<sources>();
            var builder = new UriBuilder("https://newsapi.org/v2/top-headlines");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["apiKey"] = ApiKey;
            if (!string.IsNullOrEmpty(category))
            {
                query["category"] = category;
            }
            builder.Query = query.ToString();
            string url = builder.ToString();
            var response = await _httpClient.GetStringAsync(url);
            var newsResponse = JsonSerializer.Deserialize<NewsResponse>(response);

            if (newsResponse?.sources != null)
                allAricles.AddRange(newsResponse.sources);

            return allAricles;
        }
    }
}

