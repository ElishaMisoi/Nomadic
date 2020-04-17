using Nomadic.Models;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Nomadic.ViewModels
{
    public class LocalViewModel : BaseViewModel
    {
        /// <summary>
        /// The Curent Tab Item
        /// </summary>
        Tab _currentItem;
        public Tab CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// True when location can't be detected
        /// </summary>
        bool _noLocation;
        public bool NoLocation
        {
            get { return _noLocation; }
            set
            {
                _noLocation = value;
                OnPropertyChanged();
            }
        }

        public LocalViewModel()
        {
            CurrentItem = new Tab();
            _ = LoadLocationNews();
        }

        /// <summary>
        /// Function to get news depending on location
        /// </summary>
        async Task LoadLocationNews()
        {
            IsBusy = true;

            string country = await GetLocation();

            if (country != null)
            {
                CurrentItem = new Tab { Title = country };

                await SearchArticles(CurrentItem);
            }

            IsBusy = false;
        }

        /// <summary>
        /// Function to get current location of device
        /// </summary>
        /// <returns>Country as string</returns>
        async Task<string> GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Lowest);
                var location = await Geolocation.GetLocationAsync(request).ConfigureAwait(true);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude).ConfigureAwait(true);

                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        return placemark.CountryName;

                        // You can refine location as below:

                        // placemark.AdminArea;
                        // placemark.Locality;
                        // placemark.SubAdminArea;
                        // placemark.SubLocality;
                    }
                    else
                    {
                        NoLocation = true;
                        IsBusy = false;
                    }
                }
                else
                {
                    NoLocation = true;
                    IsBusy = false;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                CurrentItem.HasError = true;
                IsBusy = false;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                NoLocation = true;
                IsBusy = false;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                NoLocation = true;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine(ex.Message);
                NoLocation = true;
                IsBusy = false;
            }

            return null;
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

                    var articles = await Helpers.NewsApiHelper.SearchArticles(new string[] { tab.Title }, page: tab.ArticlePage);

                    tab.Articles.AddRange(articles);

                    tab.IsBusy = false;
                }
                else if (isRefreshing)
                {
                    tab.IsRefreshing = true;

                    var articles = await Helpers.NewsApiHelper.SearchArticles(new string[] { tab.Title }, page: tab.ArticlePage);

                    tab.Articles.ReplaceRange(articles);

                    tab.IsRefreshing = false;
                }

                if (tab.Articles.Count == 0)
                {
                    tab.IsRefreshing = false;
                    tab.IsBusy = false;

                    if (!NoLocation)
                    {
                        tab.HasError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tab.IsRefreshing = false;
                tab.IsBusy = false;
                if (!NoLocation)
                {
                    tab.HasError = true;
                }
            }
        }

        /// <summary>
        /// Function to reload page data
        /// </summary>
        async Task Reload()
        {
            if (NoLocation)
            {
                NoLocation = false;
                await LoadLocationNews();
            }
            else
            {
                await SearchArticles(CurrentItem, isRefreshing: true);
            }
        }

        ICommand _refreshCommand = null;

        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand =
                                          new Xamarin.Forms.Command(async (object obj) => await Reload()));
            }
        }
    }
}
