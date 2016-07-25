using System;
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
        private SearchListAdapter listAdapter;
        private ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.YelpSearchResult);

            var restaurants = Intent.Extras.GetString("restaurantList") ?? "blank";

            //Need to add handling for no results found
            //var restaurantList = new List<YelpListingDto>();

            var restaurantsJson = JsonConvert.DeserializeObject<List<YelpListingDto>>(restaurants);

            listView = FindViewById<ListView>(Resource.Id.yelpSearchResultList);

            listAdapter = new SearchListAdapter(this, restaurantsJson);
            listView.Adapter = listAdapter;

            var backButton = FindViewById<ImageButton>(Resource.Id.returnToSearchButton);

            backButton.Click += (sender, e) =>
            {
                //leaving both options here until we decide which to use
                OnBackPressed();

                //var intent = new Intent(this, typeof(SearchYelpActivity));

                //StartActivity(intent);
            };

            listView.ItemClick += (sender, e) =>
            {
                var restaurant = restaurantsJson[e.Position];

                var intent = new Intent(this, typeof(RatingsViolationsComparisonActivity));
                intent.PutExtra("restaurant", JsonConvert.SerializeObject(restaurant));
                StartActivity(intent);

                //Toast.MakeText(this, restaurant.Id, ToastLength.Short).Show();
            };

        }
    }
}