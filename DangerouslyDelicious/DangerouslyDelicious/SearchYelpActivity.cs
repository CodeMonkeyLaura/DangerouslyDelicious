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

            searchYelpButton.Click += async (sender, e) =>
            {
                string url = @"https://api.yelp.com/v2/search/?term=" + restaurantSearchBox.Text + @"&location=Louisville, KY";

                //var searchResult = await GetSearchAsync(url);

                //var tempAlert = new AlertDialog.Builder(this);
                //tempAlert.SetMessage(searchResult.ToString());

                //tempAlert.SetNeutralButton("OK", delegate { });

                //tempAlert.Show();
            };
        }

        //private async Task<JsonValue> GetSearchAsync(string url)
        //{
        //    //var request = WebRequest.Create(new Uri(url));
        //    //request.ContentType = "application/json";
        //    //request.Method = "GET";

        //    var request = new RestRequest(new Uri(url), Method.GET);

        //    using (WebResponse response = await request.GetResponseAsync())
        //    {
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            var searchResultString = await Task.Run(() => JsonValue.Load(stream));

        //            return searchResultString;
        //        }
        //    }
        //}
    }
}