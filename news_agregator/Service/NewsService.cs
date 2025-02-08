using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IRepository;
using Model;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Microsoft.Extensions.Configuration;
using IRepository;

namespace Service
{
    public class NewsService
    {
        private readonly NewsApiClient _newsApiClient;
        private readonly IRepository<NewsArticle> _dbNews;


        public NewsService(IConfiguration configuration, IRepository<NewsArticle> dbNews)
        {
            var apiKey = configuration["NewsApiSettings:ApiKey"];
            _newsApiClient = new NewsApiClient(apiKey);
            _dbNews = dbNews;
        }

        public async Task<List<NewsArticle>> GetNewsAsync(string query, string category = null, string language = "uk")
        {
            var allArticles = new List<NewsArticle>();

            var articlesResponse = _newsApiClient.GetEverything(new EverythingRequest
            {
                Q = query,
                SortBy = SortBys.Popularity,
                Language = (Languages)Enum.Parse(typeof(Languages), language, true),
                From = DateTime.UtcNow.AddDays(-7),
                PageSize = 10,
                Page = 1

            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                foreach (var article in articlesResponse.Articles)
                {
                    var newsArticle = new NewsArticle
                    {
                        Title = article.Title,
                        Author = article.Author,
                        Content = article.Content,
                        Description = article.Description,
                        Url = article.Url,
                        PublishedAt = article.PublishedAt,
                        UrlToImage = article.UrlToImage,
                        Source = new Source
                        {
                            Name = article.Source.Name
                        }
                    };

                    allArticles.Add(newsArticle);

                     _dbNews.Create(newsArticle);
                    await _dbNews.Save();
                }

                

                return allArticles;
            }

            return new List<NewsArticle>();
        }

    }





}