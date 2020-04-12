using NewsAPI.Constants;
using Newtonsoft.Json;
using Nomadic.Helpers;
using Nomadic.Models;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nomadic.ViewModels
{
    public class MainFeedViewModel : BaseViewModel
    {

        #region properties

        /// <summary>
        /// TabItems that will be displayed in the UI
        /// TabItems also hold articles
        /// </summary>
        ObservableRangeCollection<Tab> _tabItems;
        public ObservableRangeCollection<Tab> TabItems
        {
            get { return _tabItems; }
            set
            {
                _tabItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The current Tab displayed in the UI
        /// </summary>
        Tab _currentTab;
        public Tab CurrentTab
        {
            get { return _currentTab; }
            set
            {
                _currentTab = value;
                OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainFeedViewModel()
        {
            TabItems = new ObservableRangeCollection<Tab>();
            CurrentTab = new Tab();
            _ = GetUserData();
        }

        #region methods

        /// <summary>
        /// Method to handle fetching of User Interests from NewsAPI
        /// </summary>
        public async Task GetUserData()
        {
            TabItems = new ObservableRangeCollection<Tab>();

            // Will make more sense when we begin to save user data locally
            List<Interest> interests = new List<Interest>
            {
                new Interest { Title = "Headlines" },
                new Interest { Title = "Business" },
                new Interest { Title = "Technology" },
                new Interest { Title = "Entertainment" },
                new Interest {  Title = "Sports" },
                new Interest { Title = "Science" },
                new Interest { Title = "Health" },
            };

            // Will make more sense when we begin to save user data locally
            foreach(var interest in interests)
            {
                TabItems.Add(new Tab
                {
                    Title = interest.Title,
                    ArticlePage = 1
                });
            }

            CurrentTab = TabItems[0];

            foreach (var tab in TabItems)
            {
                await LoadTabData(tab).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Method to Handle fetching of Articles from NewsAPI 
        /// depending on the user Interests
        /// </summary>
        /// <param name="tab">Takes in a Tab Item</param>
        /// <param name="isRefreshing">True if page is being refreshed</param>
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

                    var articles = await NewsApiHelper.SearchArticles(new string[] { tab.Title.ToLower() }, page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await NewsApiHelper.SearchArticles(new string[] { tab.Title.ToLower() }, page: tab.ArticlePage);

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
        /// Method for refreshing TabContent
        /// To be used in ListView RefreshCommand
        /// </summary>
        async Task RefreshTabContent()
        {
            await LoadTabData(CurrentTab, isRefreshing: true);
        }

        /// <summary>
        /// Method to reload all TabItems
        /// To be used in Page ReloadCommand 
        /// On the event that data failed to load
        /// e.g when a Network error occurs
        /// </summary>
        async Task ReloadData()
        {
            foreach (var tab in TabItems)
            {
                await LoadTabData(tab).ConfigureAwait(false);
            }
        }

        ICommand _refreshCommand = null;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await RefreshTabContent()));
            }
        }

        ICommand _reloadCommand = null;

        public ICommand ReloadCommand
        {
            get
            {
                return _reloadCommand ?? (_reloadCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await ReloadData()));
            }
        }

        #endregion

        public static MainFeedViewModel Instance { get; } = new MainFeedViewModel();

    }
}
