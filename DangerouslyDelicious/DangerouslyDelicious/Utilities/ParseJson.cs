using System.Collections.Generic;
using System.Json;
using System.Linq;
using DangerouslyDelicious.Dtos;

namespace DangerouslyDelicious.Utilities
{
    public class ParseJson
    {
        public static List<YelpListingDto> MakeYelpListingDto(JsonValue restaurantList)
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

        public static RestaurantInspectionDto MakeRestaurantInspectionDto(JsonValue returnString)
        {
            var returnStringResults = returnString["result"];
            var inspectionList = new List<RestaurantInspectionDto>();
            var inspectionData = new RestaurantInspectionDto();

            if (returnStringResults["total"] != 0)
            {
                var returnStringRecords = returnStringResults["records"];

                foreach (JsonValue establishment in returnStringRecords)
                {
                    var locationFound = new RestaurantInspectionDto()
                    {
                        EstablishmentId = establishment["EstablishmentId"],
                        EstablishmentName = establishment["EstablishmentName"],
                        Grade = establishment["Grade"],
                        Score = establishment["Score"],
                        InspectionDate = establishment["InspectionDate"]
                    };

                    inspectionList.Add(locationFound);
                }

                inspectionData = inspectionList.OrderByDescending(i => i.InspectionDate).ToList().FirstOrDefault();
            }

            return inspectionData;
        }

        public static List<LocationMatchDto> MakeLocationMatchDto(JsonValue returnString)
        {
            var returnStringResults = returnString["result"];
            var restaurantMatches = new List<LocationMatchDto>();

            if (returnStringResults["total"] != 0)
            {
                var returnStringRecords = returnStringResults["records"];

                foreach (JsonValue establishment in returnStringRecords)
                {
                    var foundLocation = new LocationMatchDto()
                    {
                        Name = establishment["PremiseName"],
                        StreetNumber = establishment["PermiseStreetNo"],
                        StreetName = establishment["PremiseStreet"],
                        EstablishmentId = establishment["EstablishmentId"],
                        Latitude = establishment["latitude"],
                        Longitude = establishment["longitude"]
                    };

                    restaurantMatches.Add(foundLocation);
                }
            }

            return restaurantMatches;
        }
    }
}