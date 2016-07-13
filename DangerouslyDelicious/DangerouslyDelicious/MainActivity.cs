using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DangerouslyDelicious
{
    [Activity(Label = "Dangerously Delicious", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var useCurrentButton = FindViewById<Button>(Resource.Id.useCurrentButton);
            var searchLocationButton = FindViewById<Button>(Resource.Id.searchLocationButton);

            searchLocationButton.Click += delegate
            {
                var intent = new Intent(this, typeof(SearchYelpActivity));

                StartActivity(intent);
            };
        }
    }
}

