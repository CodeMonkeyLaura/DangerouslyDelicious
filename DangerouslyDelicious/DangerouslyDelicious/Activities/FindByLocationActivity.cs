using System.IO;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Net;
using Android.OS;
using Android.Util;
using DangerouslyDelicious.Models;
using DangerouslyDelicious.Services;
using Felipecsl.GifImageViewLibrary;

namespace DangerouslyDelicious.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar")]
    public class FindByLocationActivity : Activity, ILocationListener
    {
        private static readonly string _tag = "SearchByLocationActivity";
        private LocationManager _locationManager;
        private ConnectivityManager _connectivityManager;
        private string _locationProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FindByLocation);

            var gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);
            StartGif("Radar.gif", gifImageView);

            var reqs = CheckLocationRequirements();

            if (reqs.Network || reqs.Gps)
            {
                if (reqs.Internet)
                {
                    _locationProvider = reqs.Network ? LocationManager.NetworkProvider : LocationManager.GpsProvider;
                    _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);

                    SearchYelp(_locationManager.GetLastKnownLocation(_locationProvider));
                }
                else
                {
                    ShowAlert("Internet access must be enabled to use this feature.");
                }
            }
            else
            {
                ShowAlert("Location services must be enabled to use this feature.");
            }
        }

        private void StartGif(string fileName, GifImageView viewName)
        {
            byte[] bytes;
            Stream input = Assets.Open(fileName);

            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                bytes = ms.ToArray();
            }

            viewName.SetBytes(bytes);
            viewName.StartAnimation();
        }

        private PhoneRequirements CheckLocationRequirements()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            _connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            var internetAccess = _connectivityManager.ActiveNetworkInfo;

            return new PhoneRequirements()
            {
                Gps = _locationManager.IsProviderEnabled(LocationManager.GpsProvider),
                Network = _locationManager.IsProviderEnabled(LocationManager.NetworkProvider),
                Internet = internetAccess != null && internetAccess.IsConnected
            };
        }

        private async void SearchYelp(Location location)
        {
            var searchString = @"https://api.yelp.com/v2/search?term=food&ll=" + location.Latitude + "," + location.Longitude + @"&sort=1&limit=5";

            var restaurantList = await YelpData.PerformSearch(searchString);

            var intent = new Intent(this, typeof(YelpSearchResultActivity));
            intent.PutExtra("restaurantList", (string)restaurantList);
            intent.PutExtra("searchResultHeader", "Nearest Restaurants to You");
            StartActivity(intent);
        }

        private void ShowAlert(string message)
        {
            var alert = new AlertDialog.Builder(this);
            alert.Create();
            alert.SetMessage(message);
            alert.SetPositiveButton("Oops", delegate
            {
                StartActivity(typeof(MainActivity));
            });
            alert.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug(_tag, "Started.");
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            Log.Debug(_tag, "Location changed");
        }

        public void OnProviderDisabled(string provider)
        {
            Log.Debug(_tag, provider + " disabled by user.");
        }

        public void OnProviderEnabled(string provider)
        {
            Log.Debug(_tag, provider + " enabled by user.");
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Log.Debug(_tag, provider + " availability has changed to " + status);
        }
    }
}