using Nomadic.Helpers;
using Nomadic.Models;
using Plugin.CloudFirestore;
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
    public class SavedArticlesViewModels : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// List of Articles to be displayed
        /// </summary>
        ObservableRangeCollection<Article> _articles;
        public ObservableRangeCollection<Article> Articles
        {
            get { return _articles; }
            set
            {
                _articles = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// True is Articles list from database is empty
        /// </summary>
        bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// True of ListView is refreshing
        /// </summary>
        bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently selected Article
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
        public SavedArticlesViewModels()
        {
            IsBusy = true;
            Articles = new ObservableRangeCollection<Article>();
            _ = LoadSavedItems();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Function to fetch saved articles from database
        /// </summary>
        /// <param name="isRefreshing"></param>
        async Task LoadSavedItems(bool isRefreshing = false)
        {
            if (isRefreshing && Articles.Any())
            {
                IsBusy = false;
                IsRefreshing = true;
            }

            try
            {
                string userId = await Helpers.DatabaseHelper.UserId();

                if (userId != null)
                {
                    CrossCloudFirestore.Current
                        .Instance
                        .GetCollection($"articles/{userId}/saved-articles")
                        .AddSnapshotListener((snapshot, error) =>
                        {
                            if (!snapshot.IsEmpty)
                            {
                                IsEmpty = false;

                                var articles = snapshot.ToObjects<Article>().OrderBy(s => s.SavedOn).Reverse();

                                if (Articles.Any())
                                {
                                    Articles.ReplaceRange(articles);
                                }
                                else
                                {
                                    Articles.AddRange(articles);
                                }
                            }
                            else
                            {
                                IsEmpty = true;
                            }

                            IsRefreshing = false;
                            IsBusy = false;
                        });
                }
                else
                {
                    IsBusy = false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Function to delete saved Article from database
        /// </summary>
        async Task DeleteArticle()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                await DatabaseHelper.DeleteSavedArticle(CurrentArticle.ID);
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
        /// Command to refresh SavedArticles Page data
        /// </summary>
        ICommand _refreshCommand = null;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await LoadSavedItems(true)));
            }
        }

        /// <summary>
        /// Command to delete Article from database
        /// </summary>
        ICommand _deleteArticleCommand = null;

        public ICommand DeleteArticleCommand
        {
            get
            {
                return _deleteArticleCommand ?? (_deleteArticleCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await DeleteArticle()));
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
        /// Gets an instance of this class
        /// </summary>
        public static SavedArticlesViewModels Instance { get; } = new SavedArticlesViewModels();
    }
}
