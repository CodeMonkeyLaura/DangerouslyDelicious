using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Json;
using System.Net;
using RestSharp.Portable;
using Xamarin.Auth;

namespace DangerouslyDelicious
{
    [Activity(Label = "Search")]
    public class SearchYelpActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchYelp);

            var searchYelpButton = FindViewById<Button>(Resource.Id.searchYelpButton);
            var restaurantSearchBox = FindViewById<EditText>(Resource.Id.restaurantSearchBox);

        }
    }
}