using System.Collections.Generic;
using Android.App;
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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.YelpSearchResult);

            var restaurants = Intent.Extras.GetString("restaurantList") ?? "blank";

            //Need to add handling for no results found
            //var restaurantList = new List<YelpListingDto>();

            var restaurantsJson = JsonConvert.DeserializeObject<List<YelpListingDto>>(restaurants);

            var listView = FindViewById<ListView>(Resource.Id.yelpSearchResultList);
            listView.Adapter = new SearchListAdapter(this, restaurantsJson);

            var backButton = FindViewById<ImageButton>(Resource.Id.returnToSearchButton);

            backButton.Click += (sender, e) =>
            {
                //leaving both options here until we decide which to use
                OnBackPressed();

                //var intent = new Intent(this, typeof(SearchYelpActivity));

                //StartActivity(intent);
            };
        }
    }
}