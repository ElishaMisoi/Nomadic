using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Plugin.CloudFirestore.Attributes;
using Xamarin.Forms;

namespace Nomadic.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class User : INotifyPropertyChanged
    {
        string _userID;
        [JsonProperty("uid")]
        [MapTo("uid")]
        public string UserID
        {
            get { return _userID; }
            set
            {
                _userID = value;
                OnPropertyChanged();
            }
        }

        string _email;
        [JsonProperty("email")]
        [MapTo("email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        string _name;
        [JsonProperty("name")]
        [MapTo("name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        string _imageUrl;
        [JsonProperty("picture")]
        [MapTo("picture")]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        List<Interest> _interests;
        [JsonProperty("interests")]
        [MapTo("interests")]
        public List<Interest> Interests
        {
            get { return _interests; }
            set
            {
                _interests = value;
                OnPropertyChanged();
            }
        }

        public User()
        {
            Interests = new List<Interest>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
