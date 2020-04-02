using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nomadic.Helpers
{
    public static class NewsApiHelper
    {
        static readonly NewsApiClient newsApiClient = new NewsApiClient(Constants.NewsAPIKey);

        /// <summary>
        /// This method tries to retrieve world top headlines
        /// </summary>
        /// <returns>List of Articles</returns>
        public static async Task<List<Article>> GetWorldTopHeadlines(int page = 1)
        {
            List<Article> articles = null;

            try
            {
                var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Page = page,
                    PageSize = 50,
                    Language = Languages.EN // Switch language to your preference
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    return articlesResponse.Articles;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return articles;
        }

        /// <summary>
        /// This method tries to retrieve top headlines
        /// By country. Pass Country as parameter.
        /// </summary>
        /// <returns>List of Articles</returns>
        public static async Task<List<Article>> GetCountryTopHeadlines(Countries country, int page = 1)
        {
            List<Article> articles = null;

            try
            {
                var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Page = page,
                    PageSize = 50,
                    Country = country,
                    Language = Languages.EN
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    return articlesResponse.Articles;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return articles;
        }

        /// <summary>
        /// This method tries genral search of articles
        /// Pass an array of keywords as parameter 
        /// Please just don't pass a huge array 🙄, 5 items at most
        /// </summary>
        /// <returns>List of Articles</returns>
        public static async Task<List<Article>> SearchArticles(string [] keywords, int page = 1)
        {
            /// please keep the limit of keywords to 5

            int pageSize;

            if (keywords.Length > 5)
                throw new ArgumentOutOfRangeException();

            switch (keywords.Length)
            {
                case 1:
                    pageSize = 50;
                    break;
                case 2:
                    pageSize = 25;
                    break;
                case 3:
                    pageSize = 15;
                    break;
                case 4:
                    pageSize = 12;
                    break;
                case 5:
                    pageSize = 10;
                    break;
                default:
                    pageSize = 50;
                    break;
            }

            List<Article> articles = null;

            try
            {
                foreach(var keyword in keywords)
                {
                    var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
                    {
                        Q = keyword,
                        Page = page,
                        PageSize = pageSize,
                        SortBy = SortBys.PublishedAt, // Switch this if you want to sort by popularity
                        Language = Languages.EN,
                        From = new DateTime(2018, 1, 25) // Enter a date you're comfortable with
                    });

                    if(articlesResponse.Status == Statuses.Ok)
                    {
                        foreach(var article in articlesResponse.Articles)
                        {
                            articles.Add(article);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return articles;
        }
    }
}
