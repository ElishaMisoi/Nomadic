using NewsAPI.Constants;
using Nomadic.Helpers;
using Nomadic.Models;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Nomadic.ViewModels
{
    public class InterestsViewModel : BaseViewModel
    {
        /// <summary>
        /// List of Interests
        /// </summary>
        ObservableRangeCollection<Interest> _interests;
        public ObservableRangeCollection<Interest> Interests
        {
            get { return _interests; }
            set
            {
                _interests = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// List of Interests to be used in Explore page for search
        /// </summary>
        ObservableRangeCollection<Interest> _interestsSearchList;
        public ObservableRangeCollection<Interest> InterestsSearchList
        {
            get { return _interestsSearchList; }
            set
            {
                _interestsSearchList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Search text bindinded to Explore SearchBar
        /// </summary>
        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                InterestsSearchList = FilteredRecords(SearchText);
            }
        }

        /// <summary>
        /// Selected Interet Tab
        /// </summary>
        Tab _currentItem;
        public Tab CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                OnPropertyChanged();
                _ = LoadTabData(CurrentItem);
            }
        }

        /// <summary>
        /// Selected Interest to be displayed in InterestArticlesPage 
        /// </summary>
        Interest _currentInterest;
        public Interest CurrentInterest
        {
            get { return _currentInterest; }
            set
            {
                _currentInterest = value;
                OnPropertyChanged();
            }
        }

        public InterestsViewModel()
        {
            _ = LoadInterestsList();
        }

        /// <summary>
        /// Task to get user interests
        /// </summary>
        /// <returns>Returns a list of interests</returns>
        async Task LoadInterestsList()
        {
            IsBusy = true;

            await Task.Run(() => 
            {

                Interests = new ObservableRangeCollection<Interest>
                {
                    new Interest
                    {
                        Title = "HEADLINES",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fheadlines.png?alt=media&token=b02c3f52-6862-4918-9d09-093864f5dd84"
                    },
                    new Interest
                    {
                        Title = "BUSINESS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fbusiness.png?alt=media&token=1fe5e4e4-0e14-49a9-82a2-44de47956659"
                    },
                    new Interest
                    {
                        Title = "TECHNOLOGY",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Ftechnology.png?alt=media&token=03c4f982-c3c5-475d-a343-17c4492b8892"
                    },
                    new Interest
                    {
                        Title = "ENTERTAINMENT",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fentertainment.png?alt=media&token=8594a41f-8c4f-4bd2-9c0c-790f0e2d6472"
                    },
                    new Interest
                    {
                        Title = "SPORTS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fsports.png?alt=media&token=6e7a9465-62c1-4026-b3a9-80259c77b3b5"
                    },
                    new Interest
                    {
                        Title = "SCIENCE",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fscience.png?alt=media&token=0e8e2b6d-9399-4a59-8d04-0ecea367a54c"
                    },
                    new Interest
                    {
                        Title = "HEALTH",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fhealth.png?alt=media&token=060a5be7-6528-49f6-adaa-1b202fc662ad"
                    },
                    new Interest
                    {
                        Title = "CRIME",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fcrime.png?alt=media&token=a05618e4-f6a6-4441-a6c8-0207a5dda076",
                    },
                    new Interest
                    {
                        Title = "POLITICS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fpolitics.png?alt=media&token=491e51d1-f6e8-4cc2-83be-6c6181a6421d"
                    },
                    new Interest
                    {
                        Title = "MOVIES",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fmovies.png?alt=media&token=eb222e92-2044-4c13-895b-b6f52b4bf13c"
                    },
                    new Interest
                    {
                        Title = "BOOKS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fbooks.png?alt=media&token=fc155a12-1dcb-4fec-b3ce-8eb693856035"
                    },
                    new Interest
                    {
                        Title = "GAMING",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fgaming.png?alt=media&token=ea3d621d-5410-41e9-9f4d-ae691e7b815e"
                    },
                    new Interest
                    {
                        Title = "RELATIONSHIPS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Frelationships.png?alt=media&token=b503a2d6-8a1b-4872-a1b9-70a9ad8e245f"
                    },
                    new Interest
                    {
                        Title = "TRAVEL",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Ftravel.png?alt=media&token=7c0e89a2-e65a-438c-93ac-250675ab36c0"
                    },
                    new Interest
                    {
                        Title = "AUTO",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fauto.png?alt=media&token=a881c40d-f535-48fd-9c78-a5db312f9fce"
                    },
                    new Interest
                    {
                        Title = "COOKING",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fcooking.png?alt=media&token=b5e841f0-1515-4e61-8177-39f60e3796ca"
                    },
                    new Interest
                    {
                        Title = "WEIGHT LOSS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fweight_loss.png?alt=media&token=d9d4da10-ec8b-4eb4-97ce-8c138b229956"
                    },
                    new Interest
                    {
                        Title = "DRINKS",
                        UrlToImage = "https://firebasestorage.googleapis.com/v0/b/nomadic-44ced.appspot.com/o/Interests%2Fdrinks.png?alt=media&token=c54355a7-e7f1-4132-9d25-a40040f4ba66"
                    },
                };

                foreach (var interest in Interests)
                {
                    var existingInMainFeed = MainFeedViewModel.Instance.TabItems.Where(s => s.Title.ToLower().Equals(interest.Title.ToLower())).Any();

                    if (existingInMainFeed)
                    {
                        interest.IsInterestAdded = true;
                    }
                }

                InterestsSearchList = Interests;

                IsBusy = false;

            });
        }

        async Task LoadTabData(Tab tab, bool isRefreshing = false)
        {
            switch (tab.Title.ToLower())
            {
                case "headlines":
                    await GetTopHeadlines(tab, isRefreshing).ConfigureAwait(false);
                    break;
                case "business":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Business, isRefreshing).ConfigureAwait(false);
                    break;
                case "technology":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Technology, isRefreshing).ConfigureAwait(false);
                    break;
                case "entertainment":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Entertainment, isRefreshing).ConfigureAwait(false);
                    break;
                case "sports":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Sports, isRefreshing).ConfigureAwait(false);
                    break;
                case "science":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Science, isRefreshing).ConfigureAwait(false);
                    break;
                case "health":
                    await GetWorldTopHeadlinesByCategory(tab, Categories.Health, isRefreshing).ConfigureAwait(false);
                    break;
                default:
                    await SearchArticles(tab).ConfigureAwait(false);
                    break;
            }
        }

        /// <summary>
        /// Method to fetch TopHeadlines
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task GetTopHeadlines(Tab tab, bool isRefreshing = false)
        {
            tab.HasError = false;

            try
            {
                if (tab.Articles.Count == 0)
                {
                    tab.IsBusy = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlines(page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlines(page: tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;
                    tab.HasError = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                tab.HasError = true;
            }
        }

        /// <summary>
        /// Method to fetch TopHeadlines by Category
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task GetWorldTopHeadlinesByCategory(Tab tab, Categories category, bool isRefreshing = false)
        {
            tab.HasError = false;

            try
            {
                if (tab.Articles.Count == 0)
                {
                    tab.IsBusy = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlinesByCategory(category: category, page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.GetWorldTopHeadlinesByCategory(category: category, page: tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;
                    tab.HasError = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                tab.HasError = true;
            }
        }

        /// <summary>
        /// Method to search articles depending on Interest Key words
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
        async Task SearchArticles(Tab tab, bool isRefreshing = false)
        {
            tab.HasError = false;

            try
            {
                if (tab.Articles.Count == 0)
                {
                    tab.IsBusy = true;

                    var articles = await NewsApiHelper.SearchArticles(new string[] { tab.Title }, page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.SearchArticles(new string[] { tab.Title }, tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;
                    tab.HasError = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                tab.HasError = true;
            }
        }

        /// <summary>
        /// Function to add user interest in MainFeed
        /// </summary>
        /// <param name="interest">Takes in an interest</param>
        public async Task AddUserInterest(Interest interest)
        {
            try
            {
                var addableTabItem = new Tab { Title = interest.Title, ArticlePage = 1 };
                var articles = await NewsApiHelper.SearchArticles(new string[] { interest.Title.ToLower() });
                addableTabItem.Articles.AddRange(articles);
                addableTabItem.IsBusy = false;

                MainFeedViewModel.Instance.TabItems.Insert(MainFeedViewModel.Instance.TabItems.IndexOf(MainFeedViewModel.Instance.TabItems.LastOrDefault()) + 1, addableTabItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Function to remove user interest in MainFeed
        /// </summary>
        /// <param name="interest">Takes in an interest</param>
        public void RemoveUserInterest(Interest interest)
        {
            try
            {
                var removableTabItem = MainFeedViewModel.Instance.TabItems.Where(s => s.Title.ToLower().Equals(interest.Title.ToLower())).FirstOrDefault();

                MainFeedViewModel.Instance.TabItems.Remove(removableTabItem);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Function to filter interests
        /// </summary>
        ObservableRangeCollection<Interest> FilteredRecords(string NewTextValue)
        {
            ObservableRangeCollection<Interest> filter = new ObservableRangeCollection<Interest>();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < Interests.Count; i++)
                {
                    Interest _interest = Interests[i];

                    if (_interest.Title.ToLower().Contains(NewTextValue.ToLower()))
                    {
                        filter.Add(_interest);
                    }
                }
            });

            return filter;
        }

        public static InterestsViewModel Instance { get; } = new InterestsViewModel();
    }
}
