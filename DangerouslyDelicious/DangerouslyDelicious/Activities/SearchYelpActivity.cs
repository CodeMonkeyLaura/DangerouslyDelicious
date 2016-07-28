using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.Utilities;

namespace DangerouslyDelicious.Activities
{
    [Activity(Label = "Dangerously Delicious", Icon = "@drawable/AppleWormIcon")]
    public class SearchYelpActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchYelp);

            var searchYelpButton = FindViewById<Button>(Resource.Id.searchYelpButton);
            var restaurantSearchBox = FindViewById<EditText>(Resource.Id.restaurantSearchBox);

            searchYelpButton.Click += async (sender, e) =>
            {
                var searchString = @"https://api.yelp.com/v2/search?term=" + CleanInputString.RemoveBadCharacters(restaurantSearchBox.Text) +
                       @"&location=Louisville, KY";

                var restaurantList = await PerformYelpSearch.FormatResults(searchString);

                var intent = new Intent(this, typeof(YelpSearchResultActivity));
                intent.PutExtra("restaurantList", (string)restaurantList);
                intent.PutExtra("searchResultHeader", "Search Results");
                StartActivity(intent);
            }; 
        }

    }
}