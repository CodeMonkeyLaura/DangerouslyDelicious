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
            var restaurant = JsonConvert.DeserializeObject<YelpListingDto>(restaurantToParse);

            var restaurantName = FindViewById<TextView>(Resource.Id.comparisonRestaurantName);
            restaurantName.Text = restaurant.Name;

            var restaurantAddress = FindViewById<TextView>(Resource.Id.comparisonRestaurantAddress);
            restaurantAddress.Text = restaurant.Address;

            var yelpStarsImage = FindViewById<ImageView>(Resource.Id.comparisonYelpStars);
            yelpStarsImage.SetImageBitmap(MakeBitmap.GetRatingStars(restaurant.RatingImage));

            var yelpStarsNumber = FindViewById<TextView>(Resource.Id.comparisonNumberStars);
            yelpStarsNumber.Text = $"{restaurant.Rating} Stars";

            var yelpRatings = FindViewById<TextView>(Resource.Id.comparisonNumberReviews);
            yelpRatings.Text = $"{restaurant.NumberReviews} Ratings";

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