using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DateHelper;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Nomadic.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Article : INotifyPropertyChanged
    {
        /// <summary>
        /// ID of the article, to be used when saving the 
        /// story to a database
        /// </summary>
        string _id;
        [JsonProperty("id")]
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Title of the article
        /// </summary>
        string _title;
        [JsonProperty("title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Used when awaiting fetching of article from
        /// NewsAPI. To be used to show ActivityIndicator 
        /// in the UI
        /// </summary>
        bool _isBusy;
        [JsonIgnore]
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Author of the article
        /// </summary>
        string _author;
        [JsonProperty("author")]
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Source of the article
        /// </summary>
        string _source;
        [JsonProperty("source")]
        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Short description of the article
        /// </summary>
        string _description;
        [JsonProperty("description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Url to the article
        /// </summary>
        string _url;
        [JsonProperty("url")]
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Image Url of the article
        /// </summary>
        string _urlToImage;
        [JsonProperty("url_to_image")]
        public string UrlToImage
        {
            get { return _urlToImage; }
            set
            {
                _urlToImage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The formated date string to be displayed in the UI
        /// </summary>
        string _published;
        [JsonIgnore]
        public string Published
        {
            get { return _published; }
            set
            {
                _published = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Date when the article was published
        /// </summary>
        DateTime? _publishedAt;
        [JsonProperty("published_at")]
        public DateTime? PublishedAt
        {
            get { return _publishedAt; }
            set
            {
                _publishedAt = value;
                OnPropertyChanged();
                if (PublishedAt != null)
                {
                    Published = DateTimeHelper.ReturnFeedFormatedDate(DateTimeHelper.ReturnLongFromDateTime((DateTime)PublishedAt));
                }
            }
        }

        /// <summary>
        /// To be used with DataTemplateSelector as a guide on which
        /// Template to use
        /// </summary>
        bool _isWideView;
        [JsonIgnore]
        public bool IsWideView
        {
            get { return _isWideView; }
            set
            {
                _isWideView = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property changed stuff for MVVM to notify UI that 
        /// something changed in the class and it should update
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
