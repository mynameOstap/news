using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Microsoft.Extensions.Configuration; 

namespace Service
{
    public class NewsService
    {
        private readonly NewsApiClient _newsApiClient;
        

        public NewsService(IConfiguration configuration)
        {
            var apiKey = configuration["NewsApiSettings:ApiKey"];
            _newsApiClient = new NewsApiClient(apiKey);
        }

        public async Task<List<Article>> GetNewsAsync(string query , string category = null, string language = "uk")
        {
            var allAricle = new List<Article>();
            
            var articlesResponse = _newsApiClient.GetEverything(new EverythingRequest
            {
                Q = query,
                SortBy = SortBys.Popularity,
                Language = (Languages)Enum.Parse(typeof(Languages), language, true),
                From = DateTime.UtcNow.AddDays(-7)

            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                
                allAricle.AddRange(articlesResponse.Articles);
                return articlesResponse.Articles;
            }

            return new List<Article>();


        }
    }
}