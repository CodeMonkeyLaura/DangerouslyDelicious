using System;
using System.Json;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Net;
using System.Threading.Tasks;
using SimpleOAuth;
using SimpleOAuthXamarin;


namespace DangerouslyDelicious
{
    [Activity(Label = "Search")]
    public class SearchYelpActivity : Activity
    {
        private readonly string _consumerKey = "xxxxxxx";
        private readonly string _consumerSecret = "xxxxxxx";
        private readonly string _token = "xxxxxxx";
        private readonly string _tokenSecret = "xxxxxxx";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SearchYelp);

            var searchYelpButton = FindViewById<Button>(Resource.Id.searchYelpButton);
            var restaurantSearchBox = FindViewById<EditText>(Resource.Id.restaurantSearchBox);

            searchYelpButton.Click += async (sender, e) =>
            {
                var searchString = @"https://api.yelp.com/v2/search?term=" + restaurantSearchBox.Text +
                       @"&location=Louisville, KY";

                var restaurantList = await GetSearchResults(searchString);
            }; 
        }

        private async Task<JsonValue> GetSearchResults(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.ContentType = "application/json";
            request.SignRequest(
                new Tokens
                {
                    AccessToken = _token,
                    AccessTokenSecret = _tokenSecret,
                    ConsumerKey = _consumerKey,
                    ConsumerSecret = _consumerSecret
                }
            ).WithEncryption(EncryptionMethod.HMACSHA1).InHeader();

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var returnData = await Task.Run((() => JsonObject.Load(stream)));

                    return returnData;
                }
            }
        }
    }
}