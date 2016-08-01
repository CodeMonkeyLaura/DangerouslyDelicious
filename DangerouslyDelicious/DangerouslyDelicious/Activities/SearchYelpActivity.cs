using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.Models;
using DangerouslyDelicious.Services;
using DangerouslyDelicious.Utilities;

namespace DangerouslyDelicious.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar")]
    public class SearchYelpActivity : Activity
    {
        private ConnectivityManager _connectivityManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchYelp);

            var searchYelpButton = FindViewById<Button>(Resource.Id.searchYelpButton);
            var restaurantSearchBox = FindViewById<EditText>(Resource.Id.restaurantSearchBox);
            var backHomeButton = FindViewById<Button>(Resource.Id.backHomeButton);

            searchYelpButton.Click += async (sender, e) =>
            {
                var reqs = CheckInternetRequirements();

                if (reqs.Internet)
                {
                    var searchString = @"https://api.yelp.com/v2/search?term=" +
                                       InputStringFormatting.RemoveUnsafeCharacters(restaurantSearchBox.Text) +
                                       @"&location=Louisville, KY";

                    var restaurantList = await YelpData.PerformSearch(searchString);

                    var intent = new Intent(this, typeof(YelpSearchResultActivity));
                    intent.PutExtra("restaurantList", (string) restaurantList);
                    intent.PutExtra("searchResultHeader", "Search Results");
                    StartActivity(intent);
                }
                else
                {
                    ShowAlert("Internet access must be enabled to use this feature.");
                }
            };

            backHomeButton.Click += (sender, e) =>
            {
                StartActivity(typeof(MainActivity));
            };
        }

        private PhoneRequirements CheckInternetRequirements()
        {
            _connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            var internetAccess = _connectivityManager.ActiveNetworkInfo;

            return new PhoneRequirements()
            {
                Internet = internetAccess != null && internetAccess.IsConnected
            };
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

    }
}