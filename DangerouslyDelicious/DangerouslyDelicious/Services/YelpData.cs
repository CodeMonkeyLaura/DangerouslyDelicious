using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;
using DangerouslyDelicious.Dtos;
using DangerouslyDelicious.Utilities;
using Newtonsoft.Json;

namespace DangerouslyDelicious.Services
{
    public class YelpData
    {
        public static async Task<JsonValue> PerformSearch(string url)
        {
            var searchResults = await HttpRequestSetup.MakeRequest(url, true);
            var formattedResults = MakeYelpListingDto(searchResults);

            return JsonConvert.SerializeObject(formattedResults);
        }

        private static List<YelpListingDto> MakeYelpListingDto(JsonValue restaurantList)
        {
            var yelpListingDtoList = new List<YelpListingDto>();

            var businesses = restaurantList["businesses"];

            foreach (JsonValue business in businesses)
            {
                var location = business["location"];
                var address = location["address"];
                var coordinates = location["coordinate"];

                var listing = new YelpListingDto()
                {
                    Id = business["id"],
                    Name = business["name"],
                    Address = address[0],
                    City = location["city"],
                    Latitude = coordinates["latitude"],
                    Longitude = coordinates["longitude"],
                    LocationClosed = business["is_closed"],
                    MobileUrl = business["mobile_url"],
                    Rating = business["rating"],
                    NumberReviews = business["review_count"],
                    RatingImage = business["rating_img_url_large"]
                };

                yelpListingDtoList.Add(listing);
            }

            return yelpListingDtoList;
        }
    }
}