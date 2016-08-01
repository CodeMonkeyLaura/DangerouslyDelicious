using System;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using DangerouslyDelicious.SimpleOAuthXamarin;

namespace DangerouslyDelicious.Utilities
{
    public class HttpRequestSetup
    {
        public static async Task<JsonValue> MakeRequest(string url, bool requiresYelpAuthentication = false)
        {
            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.ContentType = "application/json";

            if (requiresYelpAuthentication)
            {
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
            }

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