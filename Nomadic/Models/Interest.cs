using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nomadic.Models
{
    public class Interest : INotifyPropertyChanged
    {
        /// <summary>
        /// Title of the Interest to be used to fetch data 
        /// from NewsAPI
        /// </summary>
        string _title;
        [JsonProperty("title")]
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
        /// Image Url of the Interest
        /// </summary>
        [JsonProperty("url_to_image")]
        public string UrlToImage { get; set; }

        string _btnIcon;
        [JsonIgnore]
        public string BtnIcon
        {
            get { return _btnIcon; }
            set
            {
                _btnIcon = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// To be used to check if user has already selected
        /// The specific Interest
        /// </summary>
        bool _isInterestAdded;
        [JsonIgnore]
        public bool IsInterestAdded
        {
            get { return _isInterestAdded; }
            set
            {
                _isInterestAdded = value;
                OnPropertyChanged();
                if (IsInterestAdded)
                {
                    // Check Icon
                    BtnIcon = "\uf00c";
                }
                else
                {
                    // Plus icon
                    BtnIcon = "\uf067";
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Interest()
        {
            IsInterestAdded = false;
        }

        /// <summary>
        /// MVVM stuff
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
