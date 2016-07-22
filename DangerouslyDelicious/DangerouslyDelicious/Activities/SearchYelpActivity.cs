using System;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using DangerouslyDelicious.SimpleOAuthXamarin;
using DangerouslyDelicious.Utilities;
using Newtonsoft.Json;

namespace DangerouslyDelicious.Activities
{
    [Activity(Label = "Dangerously Delicious", Icon = "@drawable/AppleWormIcon")]
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
                var searchString = @"https://api.yelp.com/v2/search?term=" + restaurantSearchBox.Text +
                       @"&location=Louisville, KY";

                var restaurantList = await GetSearchResults(searchString);

                var restaurantListParsed = ParseYelpJson.MakeDto(restaurantList);

                var intent = new Intent(this, typeof(YelpSearchResultActivity));
                intent.PutExtra("restaurantList", JsonConvert.SerializeObject(restaurantListParsed));
                StartActivity(intent);
            }; 
        }

        private async Task<JsonValue> GetSearchResults(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.ContentType = "application/json";

            var signingInfo = new YelpToken();

            request.SignRequest(
                new Tokens
                {
                    AccessToken = signingInfo.Token,
                    AccessTokenSecret = signingInfo.TokenSecret,
                    ConsumerKey = signingInfo.ConsumerKey,
                    ConsumerSecret = signingInfo.ConsumerSecret
                }
            ).WithEncryption(EncryptionMethod.HMACSHA1).InHeader();

            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var returnData = await Task.Run((() => JsonValue.Load(stream)));

                    return returnData;
                }
            }
        }

    }
}