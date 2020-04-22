using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nomadic.AppSettings;
using Nomadic.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomadic.Helpers
{
    public static class DatabaseHelper
    {
        public static async Task<User> GetUser(string userId)
        {
            try
            {
                var snapshot = await CrossCloudFirestore.Current
                                            .Instance
                                            .GetDocument($"users/{userId}")
                                            .GetDocumentAsync();

                return snapshot.ToObject<User>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static async Task AddUser(User user)
        {
            try
            {
                await CrossCloudFirestore.Current
                        .Instance
                        .GetDocument($"users/{user.UserID}")
                        .SetDataAsync(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static List<Interest> GetSavedInterestsList()
        {
            string userInterests = Settings.GetSetting(Settings.AppPrefrences.Interests);

            if (userInterests != null)
            {
                return JsonConvert.DeserializeObject<List<Interest>>(userInterests);
            }

            return null;
        }

        public static async Task UpdateUserInterests(List<Interest> interests)
        {
            try
            {
                string userId = await UserId();

                if (userId != null)
                {
                    await CrossCloudFirestore.Current
                        .Instance
                        .GetDocument($"users/{userId}")
                        .UpdateDataAsync(new { interests });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task SaveArticle(Article article)
        {
            try
            {
                string userId = await UserId();

                if (userId != null)
                {
                    DialogsHelper.HandleDialogMessage(DialogsHelper.Errors.Defined, "Article Saved.");

                    article.ID = ArticleId(article.Url);
                    article.SavedOn = DateHelper.DateTimeHelper.ReturnCurrentTimeInLong();

                    await CrossCloudFirestore.Current.Instance
                        .GetDocument($"articles/{userId}/saved-articles/{article.ID}")
                        .SetDataAsync(article);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task DeleteSavedArticle(string articleId)
        {
            try
            {
                string userId = await UserId();

                if (userId != null)
                {
                    await CrossCloudFirestore.Current.Instance
                        .GetDocument($"articles/{userId}/saved-articles/{articleId}")
                        .DeleteDocumentAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task<string> UserId()
        {
            var isLoggedIn = Settings.GetSetting(Settings.AppPrefrences.IsLoggedIn);

            if (isLoggedIn == null || isLoggedIn == "False")
            {
                DialogsHelper.HandleDialogMessage(DialogsHelper.Errors.Defined, "You're not logged in. Log in to perform this action.");
            }
            else
            {
                string userJson = await Settings.GetSecureSetting(Settings.AppPrefrences.User);
                var userObject = JsonConvert.DeserializeObject<User>(userJson);
                return userObject.UserID;
            }

            return null;
        }

        static string ArticleId(string value)
        {
            value = value
                .Replace("https://", "")
                .Replace("http://", "")
                .Replace("www", "")
                .Replace("/", "")
                .Replace(".", "")
                .Replace("-", "");

            return value.Length <= 24 ? value : value.Substring(0, 24);
        }
    }
}
