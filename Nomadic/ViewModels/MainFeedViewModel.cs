using NewsAPI.Constants;
using Newtonsoft.Json;
using Nomadic.AppSettings;
using Nomadic.Helpers;
using Nomadic.Models;
using PSC.Xamarin.MvvmHelpers;
using Rg.Plugins.Popup.Services;
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

        /// <summary>
        /// Current Article used in popups
        /// </summary>
        Article _currentArticle;
        public Article CurrentArticle
        {
            get { return _currentArticle; }
            set
            {
                _currentArticle = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainFeedViewModel()
        {
            TabItems = new ObservableRangeCollection<Tab>();
            CurrentTab = new Tab();
            _ = GetUserData();
        }

        #endregion

        #region methods

        /// <summary>
        /// Method to handle fetching of User Interests from NewsAPI
        /// </summary>
        public async Task GetUserData()
        {
            if (TabItems.Any())
            {
                TabItems.Clear();
            }

            var userInterestsList = DatabaseHelper.GetSavedInterestsList();

            if (userInterestsList != null && userInterestsList.Any())
            {
                foreach (var interest in userInterestsList)
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
            else
            {
                // Save locally and load user interests if null

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

                var interestsJson = JsonConvert.SerializeObject(interests);
                Settings.AddSetting(Settings.AppPrefrences.Interests, interestsJson);

                await GetUserData();
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

        /// <summary>
        /// Function to save article to database
        /// </summary>
        async Task SaveArticle()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                await DatabaseHelper.SaveArticle(CurrentArticle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Function to open share modal
        /// </summary>
        async Task ShareArticle()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                await DialogsHelper.ShareText($"Check out this article: \n\n {CurrentArticle.Url}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Function to close popup
        /// </summary>
        async Task ClosePopup()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Command for refreshing TabContent
        /// </summary>
        ICommand _refreshCommand = null;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await RefreshTabContent()));
            }
        }

        /// <summary>
        /// Command to reload all TabItems
        /// </summary>
        ICommand _reloadCommand = null;

        public ICommand ReloadCommand
        {
            get
            {
                return _reloadCommand ?? (_reloadCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await ReloadData()));
            }
        }

        /// <summary>
        /// Command to save Article from database
        /// </summary>
        ICommand _saveArticleCommand = null;

        public ICommand SaveArticleCommand
        {
            get
            {
                return _saveArticleCommand ?? (_saveArticleCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await SaveArticle()));
            }
        }

        /// <summary>
        /// Command to open share modal
        /// </summary>
        ICommand _shareArticleCommand = null;

        public ICommand ShareArticleCommand
        {
            get
            {
                return _shareArticleCommand ?? (_shareArticleCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await ShareArticle()));
            }
        }

        /// <summary>
        /// Command to close popup
        /// </summary>
        ICommand _closePopupCommand = null;

        public ICommand ClosePopupCommand
        {
            get
            {
                return _closePopupCommand ?? (_closePopupCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await ClosePopup()));
            }
        }

        #endregion

        /// <summary>
        /// Gets an Instance of this class
        /// </summary>
        public static MainFeedViewModel Instance { get; } = new MainFeedViewModel();

    }
}
