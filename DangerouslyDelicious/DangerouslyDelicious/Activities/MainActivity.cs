using Android.App;
using Android.OS;
using Android.Widget;

namespace DangerouslyDelicious.Activities
{
    [Activity(Label = "Dangerously Delicious", MainLauncher = true, Icon = "@drawable/AppleWormIcon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var useCurrentButton = FindViewById<Button>(Resource.Id.useCurrentButton);
            var searchLocationButton = FindViewById<Button>(Resource.Id.searchLocationButton);

            searchLocationButton.Click += (sender, e) =>
            {
                StartActivity(typeof(SearchYelpActivity));
            };

            useCurrentButton.Click += (sender, e) =>
            { 
                StartActivity(typeof(FindByLocationActivity));
            };
        }
    }
}

