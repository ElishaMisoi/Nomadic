using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DateHelper;
using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Xamarin.Forms;

namespace Nomadic.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Article : INotifyPropertyChanged
    {
        string _id;
        [JsonProperty("id")]
        [MapTo("id")]
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        string _title;
        [JsonProperty("title")]
        [MapTo("title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        bool _isBusy;
        [JsonIgnore]
        [Ignored]
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        string _author;
        [JsonProperty("author")]
        [MapTo("author")]
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        string _source;
        [JsonProperty("source")]
        [MapTo("source")]
        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        string _description;
        [JsonProperty("description")]
        [MapTo("description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        string _url;
        [JsonProperty("url")]
        [MapTo("url")]
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        string _urlToImage;
        [JsonProperty("url_to_image")]
        [MapTo("url_to_image")]
        public string UrlToImage
        {
            get { return _urlToImage; }
            set
            {
                _urlToImage = value;
                OnPropertyChanged();
            }
        }

        string _published;
        [JsonIgnore]
        [Ignored]
        public string Published
        {
            get { return _published; }
            set
            {
                _published = value;
                OnPropertyChanged();
            }
        }

        DateTime? _publishedAt;
        [JsonProperty("published_at")]
        [MapTo("published_at")]
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

        long _savedOn;
        [JsonProperty("saved_on")]
        [MapTo("saved_on")]
        public long SavedOn
        {
            get { return _savedOn; }
            set
            {
                _savedOn = value;
                OnPropertyChanged();
            }
        }

        bool _isWideView;
        [JsonIgnore]
        [Ignored]
        public bool IsWideView
        {
            get { return _isWideView; }
            set
            {
                _isWideView = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
