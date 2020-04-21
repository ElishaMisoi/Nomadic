using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    }
}
