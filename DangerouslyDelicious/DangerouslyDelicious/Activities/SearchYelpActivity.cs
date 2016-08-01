using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.Services;
using DangerouslyDelicious.Utilities;

namespace DangerouslyDelicious.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar")]
    public class SearchYelpActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchYelp);

            var searchYelpButton = FindViewById<Button>(Resource.Id.searchYelpButton);
            var restaurantSearchBox = FindViewById<EditText>(Resource.Id.restaurantSearchBox);
            var backHomeButton = FindViewById<Button>(Resource.Id.backHomeButton);

            searchYelpButton.Click += async (sender, e) =>
            {
                var searchString = @"https://api.yelp.com/v2/search?term=" + InputStringFormatting.RemoveUnsafeCharacters(restaurantSearchBox.Text) +
                       @"&location=Louisville, KY";

                var restaurantList = await YelpData.PerformSearch(searchString);

                var intent = new Intent(this, typeof(YelpSearchResultActivity));
                intent.PutExtra("restaurantList", (string)restaurantList);
                intent.PutExtra("searchResultHeader", "Search Results");
                StartActivity(intent);
            };

            backHomeButton.Click += (sender, e) =>
            {
                StartActivity(typeof(MainActivity));
            };
        }

    }
}