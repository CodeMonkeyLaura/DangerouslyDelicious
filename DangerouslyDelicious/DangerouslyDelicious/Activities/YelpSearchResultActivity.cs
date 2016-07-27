using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.Adapters;
using DangerouslyDelicious.Dtos;
using Newtonsoft.Json;

namespace DangerouslyDelicious.Activities
{
    [Activity(Label = "Dangerously Delicious", Icon = "@drawable/AppleWormIcon")]
    public class YelpSearchResultActivity : Activity
    {
        private SearchListAdapter _listAdapter;
        private ListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.YelpSearchResult);

            var restaurants = Intent.Extras.GetString("restaurantList") ?? "blank";
            var restaurantsJson = new List<YelpListingDto>();

            if (restaurants != "blank")
            {
                restaurantsJson = JsonConvert.DeserializeObject<List<YelpListingDto>>(restaurants);
            }

            var searchHeader = Intent.Extras.GetString("searchResultHeader") ?? "No results found!";

            if (restaurantsJson.Count == 0)
            {
                searchHeader = "No results found!";
            }

            FindViewById<TextView>(Resource.Id.searchResultHeader).Text = searchHeader;

            _listView = FindViewById<ListView>(Resource.Id.yelpSearchResultList);
            _listAdapter = new SearchListAdapter(this, restaurantsJson);
            _listView.Adapter = _listAdapter;

            var backButton = FindViewById<ImageButton>(Resource.Id.returnToSearchButton);

            backButton.Click += (sender, e) =>
            {
                if (!searchHeader.StartsWith("Search"))
                {
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    OnBackPressed();
                }
            };

            _listView.ItemClick += (sender, e) =>
            {
                var restaurant = restaurantsJson[e.Position];

                var intent = new Intent(this, typeof(RatingsViolationsComparisonActivity));
                intent.PutExtra("restaurant", JsonConvert.SerializeObject(restaurant));
                StartActivity(intent);
            };

        }
    }
}