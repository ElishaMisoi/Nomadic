using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nomadic.Models
{
    public class Tab : BaseViewModel
    {
        /// <summary>
        /// Title of the tab item
        /// </summary>
        string _title;
        public string Title
        {
            get { return _title.ToUpper(); }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current page from NewsAPI (To be used for pagination) 
        /// </summary>
        int _articlePage;
        public int ArticlePage
        {
            get { return _articlePage; }
            set
            {
                _articlePage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Set to true on the event that the tab has an error
        /// e.g Network error
        /// </summary>
        bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// To be used for pull to refresh functionality
        /// of ListView
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
        /// List of Articles
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
        /// Constructor
        /// </summary>
        public Tab()
        {
            Articles = new ObservableRangeCollection<Article>();
            IsBusy = true;
        }
    }
}
