using System.Collections.Generic;
using System.Json;
using DangerouslyDelicious.Dtos;

namespace DangerouslyDelicious.Utilities
{
    public class ParseYelpJson
    {
        public static List<YelpListingDto> MakeDto(JsonValue restaurantList)
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