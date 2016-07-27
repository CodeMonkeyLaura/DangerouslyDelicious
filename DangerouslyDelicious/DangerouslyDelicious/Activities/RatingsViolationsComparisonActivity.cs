using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.Dtos;
using DangerouslyDelicious.Utilities;
using Newtonsoft.Json;

namespace DangerouslyDelicious.Activities
{
    [Activity(Label = "Time to Decide!", Icon = "@drawable/AppleWormIcon")]
    public class RatingsViolationsComparisonActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.RatingsViolationsComparison);

            var restaurantToParse = Intent.Extras.GetString("restaurant") ?? "blank";
            var restaurant = new YelpListingDto();

            if (restaurantToParse != "blank")
            {
                restaurant = JsonConvert.DeserializeObject<YelpListingDto>(restaurantToParse);
            }

            FindViewById<TextView>(Resource.Id.comparisonRestaurantName).Text = restaurant.Name;
            FindViewById<TextView>(Resource.Id.comparisonRestaurantAddress).Text = restaurant.Address;
            FindViewById<ImageView>(Resource.Id.comparisonYelpStars).SetImageBitmap(MakeBitmap.GetRatingStars(restaurant.RatingImage));
            FindViewById<TextView>(Resource.Id.comparisonNumberStars).Text = $"{restaurant.Rating} Stars";
            FindViewById<TextView>(Resource.Id.comparisonNumberReviews).Text = $"{restaurant.NumberReviews} Ratings";

            var readReviewsButton = FindViewById<Button>(Resource.Id.readReviewsButton);

            readReviewsButton.Click += (sender, e) =>
            {
                var uri = Android.Net.Uri.Parse(restaurant.MobileUrl);

                var intent = new Intent(Intent.ActionView, uri);

                StartActivity(intent);
            };
        }
    }
}