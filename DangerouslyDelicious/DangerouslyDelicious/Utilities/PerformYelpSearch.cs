using System;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using DangerouslyDelicious.SimpleOAuthXamarin;
using Newtonsoft.Json;

namespace DangerouslyDelicious.Utilities
{
    public class PerformYelpSearch
    {
        public static async Task<JsonValue> FormatResults(string url)
        {
            var searchResults = await GetSearchResults(url);
            var formattedResults = ParseYelpJson.MakeDto(searchResults);

            return JsonConvert.SerializeObject(formattedResults);
        }

        private static async Task<JsonValue> GetSearchResults(string url)
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
                    var returnData = await Task.Run(() => JsonValue.Load(stream));

                    return returnData;
                }
            }
        }
    }
}