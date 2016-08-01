using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.Dtos;
using DangerouslyDelicious.Utilities;
using Newtonsoft.Json;

namespace DangerouslyDelicious.Activities
{
    [Activity(Theme = "@android:style/Theme.NoTitleBar")]
    public class RatingsViolationsComparisonActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.RatingsViolationsComparison);

            var restaurantToParse = Intent.Extras.GetString("restaurant") ?? "blank";
            var restaurant = new YelpListingDto();

            var inspectionToParse = Intent.Extras.GetString("inspectionData") ?? "blank";

            if (restaurantToParse != "blank")
            {
                restaurant = JsonConvert.DeserializeObject<YelpListingDto>(restaurantToParse);
            }

            FindViewById<TextView>(Resource.Id.comparisonGrade).Text = "No inspection data found.";

            if (inspectionToParse != "blank")
            {
                var inspection = JsonConvert.DeserializeObject<RestaurantInspectionDto>(inspectionToParse);

                if (inspection.EstablishmentId != 0)
                {
                    FindViewById<TextView>(Resource.Id.comparisonScore).Text = $"Overall Score: {inspection.Score}";
                    FindViewById<TextView>(Resource.Id.comparisonGrade).Text = $"Inspection Grade: {inspection.Grade}";
                    FindViewById<TextView>(Resource.Id.comparisonInspectedOn).Text = "Inspected On";
                    FindViewById<TextView>(Resource.Id.comparisonInspectionDate).Text =
                        FormatDate(inspection.InspectionDate);
                }
            }


            FindViewById<TextView>(Resource.Id.comparisonRestaurantName).Text = restaurant.Name;
            FindViewById<TextView>(Resource.Id.comparisonRestaurantAddress).Text = restaurant.Address;

            FindViewById<ImageView>(Resource.Id.comparisonYelpStars).SetImageBitmap(BitmapDownloader.GetRatingStars(restaurant.RatingImage));
            FindViewById<TextView>(Resource.Id.comparisonNumberStars).Text = $"{restaurant.Rating} Stars";
            FindViewById<TextView>(Resource.Id.comparisonNumberReviews).Text = $"{restaurant.NumberReviews} Ratings";

            var readReviewsButton = FindViewById<Button>(Resource.Id.readReviewsButton);
            var startOverButton = FindViewById<Button>(Resource.Id.startOverButton);

            readReviewsButton.Click += (sender, e) =>
            {
                var uri = Android.Net.Uri.Parse(restaurant.MobileUrl);

                var intent = new Intent(Intent.ActionView, uri);

                StartActivity(intent);
            };

            startOverButton.Click += (sender, e) =>
            {
                StartActivity(typeof(MainActivity));
            };
        }

        private string FormatDate(string inspectionDate)
        {
            var year = inspectionDate.Substring(0, 4);
            var month = inspectionDate.Substring(5, 2);
            var day = inspectionDate.Substring(8, 2);

            return $"{month}/{day}/{year}";
        }
    }
}