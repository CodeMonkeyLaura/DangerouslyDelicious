using DangerouslyDelicious.Dtos;
using Newtonsoft.Json;
using System.Net;

namespace DangerouslyDelicious.Services
{
    public class RestaurantInspectionService
    {
        public static RestaurantInspectionDto GetRestaurantInspectionInfoByName(string name)
        {
            var request = GetRestaurantRequest(name);
            var response = GetRestaurantResponse(request);
            return DeserializeJsonResponse(CleanJsonString(response));
        }

        private static HttpWebRequest GetRestaurantRequest(string restaurantNamePart)
        {
            // TODO: find a better way to do this than by "namePart".
            // this is the only examply method I could find to call, and the names
            // between Yelp and the Louisville API don't always line up.
            // For example "Royals Hot Chicken" from Yelp does not resolve to finding
            // "Royals Chicken" as it is found in the Louisiville API.
            // Also, a search for Wendy's would have to include which location.  For now, this
            // always returns the information for the first restaurant in the list when many
            // are returned, and this is most likely not going to be the one the user clicked on.

            HttpWebRequest request = 
                System.Net.WebRequest.Create(
                    string.Format("http://api.louisvilleky.gov/api/FoodService/FindFoodServiceScoresByName?namePart={0}", restaurantNamePart)) as System.Net.HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            return request;
        }

        private static string GetRestaurantResponse(HttpWebRequest request)
        {
            var response = string.Empty;

            using (HttpWebResponse httpResponse = request.GetResponse() as System.Net.HttpWebResponse)
            {
                using (var reader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }
            }

            return response;
        }

        private static RestaurantInspectionDto DeserializeJsonResponse(string contentToDeserialize)
        {
            if (!ContainsData(contentToDeserialize))
            {
                return new RestaurantInspectionDto();
            }

            return JsonConvert.DeserializeObject<RestaurantInspectionDto>(contentToDeserialize);
        }

        public static string CleanJsonString(string jsonToClean)
        {
            if (!ContainsData(jsonToClean))
            {
                return jsonToClean;
            }

            var indexOfOpeningCurlyBrace = jsonToClean.IndexOf("{");
            jsonToClean = jsonToClean.Substring(indexOfOpeningCurlyBrace, jsonToClean.Length - indexOfOpeningCurlyBrace);
            jsonToClean = jsonToClean.Substring(0, jsonToClean.IndexOf("}") + 1);
            return jsonToClean;
        }

        private static bool ContainsData(string json)
        {
            return json.Contains("{");
        }
    }
}