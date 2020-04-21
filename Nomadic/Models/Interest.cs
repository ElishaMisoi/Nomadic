using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using PSC.Xamarin.MvvmHelpers;
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
        string _title;
        [JsonProperty("title")]
        [MapTo("title")]
        public string Title
        {
            get { return _title.ToUpper(); }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty("url_to_image")]
        [MapTo("url_to_image")]
        public string UrlToImage { get; set; }

        string _btnIcon;
        [JsonIgnore]
        [Ignored]
        public string BtnIcon
        {
            get { return _btnIcon; }
            set
            {
                _btnIcon = value;
                OnPropertyChanged();
            }
        }

        bool _isInterestAdded;
        [JsonIgnore]
        [Ignored]
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

        public Interest()
        {
            IsInterestAdded = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
