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
        /// Sources for Headlines
        /// </summary>
        static readonly List<string> HeadlinesNewsSources = new List<string>
        {
            // General

            "abc-news-au",
            "al-jazeera-english",
            "associated-press",
            "bbc-news",
            "bloomberg",
            "cnn",
            "google-news",
            "newsweek",
            "the-washington-post",
            "time",
            "usa-today",

            // Unique to headlines

            "breitbart-news",
            "independent",
            "reuters",
            "the-huffington-post",
            "the-times-of-india",
        };

        /// <summary>
        /// Sources for business news
        /// </summary>
        static readonly List<string> BusinessNewsSources = new List<string>
        {
            "business-insider",
            "business-insider-uk",
            "cnbc",
            "financial-times",
            "fortune",
            "the-wall-street-journal",
        };

        static readonly List<string> TechnologyNewsSources = new List<string>
        {
            "ars-technica",
            "engadget",
            "hacker-news",
            "recode",
            "polygon",
            "techcrunch",
            "techradar",
            "the-next-web",
            "the-verge",
            "wired-de",
        };

        /// <summary>
        /// Sources for enternainment
        /// </summary>
        static readonly List<string> EntertainmentNewsSources = new List<string>
        {
            "buzzfeed",
            "entertainment-weekly",
            "mashable",
            "mtv-news",
            "mtv-news-uk",
            "new-york-magazine",
            "polygon",
            "ign",
            "reddit-r-all",
            "the-lad-bible",
        };

        /// <summary>
        /// Sources for sports
        /// </summary>
        static readonly List<string> SportsNewsSources = new List<string>
        {
            "bbc-sport",
            "espn",
            "football-italia",
            "four-four-two",
            "fox-sports",
            "nfl-news",
            "talksport",
            "the-sport-bible",
        };

        /// <summary>
        /// sources for science
        /// </summary>
        static readonly List<string> ScienceNewsSources = new List<string>
        {
            "national-geographic",
            "new-scientist",
            "ars-technica",
            "engadget",
            "hacker-news",
            "the-verge",
        };


        static List<Models.Article> articles;

        /// <summary>
        /// This method tries to retrieve world top headlines
        /// </summary>
        /// <returns>List of Articles</returns>
        public static async Task<List<Models.Article>> GetWorldTopHeadlines(int page = 1)
        {
            articles = new List<Models.Article>();

            try
            {
                var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Sources = HeadlinesNewsSources,
                    Page = page,
                    PageSize = 50,
                    Language = Languages.EN // Switch language to your preference
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    articlesResponse.Articles.Reverse();

                    foreach (var article in articlesResponse.Articles)
                    {
                        var postition = articlesResponse.Articles.IndexOf(article);

                        articles.Add(new Models.Article
                        {
                            Author = article.Author,
                            Description = article.Description,
                            PublishedAt = article.PublishedAt,
                            Source = $"Source: {article.Source.Name} ",
                            Title = article.Title,
                            Url = article.Url,
                            UrlToImage = article.UrlToImage,
                            IsWideView = IsWideView(postition)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return articles;
        }

        /// <summary>
        /// This method tries to retrieve world top headlines by category
        /// </summary>
        /// <returns>List of Articles</returns>
        public static async Task<List<Models.Article>> GetWorldTopHeadlinesByCategory(Categories category, int page = 1)
        {
            articles = new List<Models.Article>();

            List<string> sources;

            switch (category)
            {
                case Categories.Business:
                    sources = BusinessNewsSources;
                    break;
                case Categories.Entertainment:
                    sources = EntertainmentNewsSources;
                    break;
                case Categories.Science:
                    sources = ScienceNewsSources;
                    break;
                case Categories.Sports:
                    sources = SportsNewsSources;
                    break;
                case Categories.Technology:
                    sources = TechnologyNewsSources;
                    break;
                default:
                    sources = HeadlinesNewsSources;
                    break;
            }

            try
            {
                ArticlesResult articlesResponse;

                if (category == Categories.Health)
                {
                    articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                    {
                        Category = category,
                        Page = page,
                        PageSize = 50,
                        Language = Languages.EN // Switch language to your preference
                    });
                }
                else
                {
                    articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                    {
                        Sources = sources,
                        Page = page,
                        PageSize = 50,
                        Language = Languages.EN // Switch language to your preference
                    });
                }

                if (articlesResponse.Status == Statuses.Ok)
                {
                    articlesResponse.Articles.Reverse();

                    foreach (var article in articlesResponse.Articles)
                    {
                        var postition = articlesResponse.Articles.IndexOf(article);

                        articles.Add(new Models.Article
                        {
                            Author = article.Author,
                            Description = article.Description,
                            PublishedAt = article.PublishedAt,
                            Source = $"Source: {article.Source.Name} ",
                            Title = article.Title,
                            Url = article.Url,
                            UrlToImage = article.UrlToImage,
                            IsWideView = IsWideView(postition)
                        });
                    }
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
        public static async Task<List<Models.Article>> GetCountryTopHeadlines(Countries country, int page = 1)
        {
            articles = new List<Models.Article>();

            try
            {
                var articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
                {
                    Sources = HeadlinesNewsSources,
                    Page = page,
                    PageSize = 50,
                    Country = country,
                    Language = Languages.EN // Switch language to your preference
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    articlesResponse.Articles.Reverse();

                    foreach (var article in articlesResponse.Articles)
                    {
                        var postition = articlesResponse.Articles.IndexOf(article);

                        articles.Add(new Models.Article
                        {
                            Author = article.Author,
                            Description = article.Description,
                            PublishedAt = article.PublishedAt,
                            Source = $"Source: {article.Source.Name} ",
                            Title = article.Title,
                            Url = article.Url,
                            UrlToImage = article.UrlToImage,
                            IsWideView = IsWideView(postition)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return articles;
        }

        /// <summary>
        /// This method tries general search of articles
        /// Pass an array of keywords as parameter 
        /// Please just don't pass a huge array 🙄, 5 items at most
        /// </summary>
        /// <returns>List of Articles</returns>
        public static async Task<List<Models.Article>> SearchArticles(string[] keywords, int page = 1)
        {
            /// please keep the limit of keywords to 5

            int pageSize;

            if (keywords.Length > 5)
                throw new ArgumentOutOfRangeException("Can you just keep a maximum of 5 keywords? Okay? Great :)");

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

            articles = new List<Models.Article>();

            try
            {
                foreach (var keyword in keywords)
                {
                    var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
                    {
                        Q = keyword,
                        Page = page,
                        PageSize = pageSize, // Switch this if you want to sort by popularity
                        Language = Languages.EN, // Switch language to your preference
                    });

                    if (articlesResponse.Status == Statuses.Ok)
                    {
                        articlesResponse.Articles.Reverse();

                        foreach (var article in articlesResponse.Articles)
                        {
                            var postition = articlesResponse.Articles.IndexOf(article);

                            articles.Add(new Models.Article
                            {
                                Author = article.Author,
                                Description = article.Description,
                                PublishedAt = article.PublishedAt,
                                Source = $"Source: {article.Source.Name} ",
                                Title = article.Title,
                                Url = article.Url,
                                UrlToImage = article.UrlToImage,
                                IsWideView = IsWideView(postition)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return articles;
        }

        /// <summary>
        /// Used to determine is article should be shown as WideView
        /// </summary>
        /// <param name="articlePosition">Takes in articles positin in a list</param>
        /// <returns>Returns if is WideView</returns>
        static bool IsWideView(int articlePosition)
        {
            if (articlePosition == 0)
                return true;

            if (articlePosition % 2 == 0 && articlePosition % 3 == 0)
                return true;

            return false;
        }
    }
}
