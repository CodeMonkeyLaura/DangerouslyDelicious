using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Util;
using DangerouslyDelicious.Utilities;

namespace DangerouslyDelicious.Activities
{
    [Activity(Label="Dangerously Delicious", Icon = "@drawable/AppleWormIcon")]
    public class FindByLocationActivity : Activity, ILocationListener
    {
        private static readonly string _tag = "SearchByLocationActivity";
        private LocationManager _manager;
        private string _locationProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FindByLocation);

            _manager = (LocationManager)GetSystemService(LocationService);

            _locationProvider = _manager.IsProviderEnabled(LocationManager.NetworkProvider) ? LocationManager.NetworkProvider : LocationManager.PassiveProvider;

            _manager.RequestLocationUpdates(_locationProvider, 0, 0, this);

            SearchYelp(_manager.GetLastKnownLocation(_locationProvider));

        }

        private async void SearchYelp(Location location)
        {
            var searchString = @"https://api.yelp.com/v2/search?term=food&ll=" + location.Latitude + "," + location.Longitude + @"&sort=1&limit=5";

            var restaurantList = await PerformYelpSearch.FormatResults(searchString);

            var intent = new Intent(this, typeof(YelpSearchResultActivity));
            intent.PutExtra("restaurantList", (string)restaurantList);
            intent.PutExtra("searchResultHeader", "Nearest Restaurants to You");
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug(_tag, "Started.");
        }

        protected override void OnPause()
        {
            base.OnPause();
            _manager.RemoveUpdates(this);
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