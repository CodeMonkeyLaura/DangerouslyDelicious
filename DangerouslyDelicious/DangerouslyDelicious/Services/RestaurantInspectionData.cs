using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using DangerouslyDelicious.Dtos;
using System.Threading.Tasks;
using DangerouslyDelicious.Utilities;

namespace DangerouslyDelicious.Services
{
    public class RestaurantInspectionData
    {
        public static async Task<RestaurantInspectionDto> MatchYelpAndEstablishment(YelpListingDto restaurant)
        {
            var establishmentId = default(int);
            var restaurantLatitude = TruncateCoords(restaurant.Latitude);
            var restaurantLongitude = TruncateCoords(restaurant.Longitude);

            var restaurantMatches = await MatchRestaurant(restaurant);

            foreach (var match in restaurantMatches)
            {
                var matchLatitude = TruncateCoords(match.Latitude);
                var matchLongitude = TruncateCoords(match.Longitude);

                if (matchLatitude == restaurantLatitude || matchLongitude == restaurantLongitude)
                {
                    if (match.Name.Substring(0,1).ToUpper() == restaurant.Name.Substring(0,1).ToUpper())
                    {
                        establishmentId = match.EstablishmentId;
                    }
                }
            }

            var inspectionData = new RestaurantInspectionDto();

            if (establishmentId != default(int))
            {
                inspectionData = await GetInspectionData(establishmentId);
            }

            return inspectionData;
        }

        private static async Task<List<LocationMatchDto>> MatchRestaurant(YelpListingDto restaurant)
        {
            var streetNumber = TruncateAddress(restaurant);
            var findEstablishment =
                @"http://data.louisvilleky.gov/api/action/datastore/search.json?resource_id=406f03bb-a022-4b6c-9460-8f0826b91a2b&filters[PermiseStreetNo]=" +
                streetNumber;

            var returnString = await HttpRequestSetup.MakeRequest(findEstablishment);
            return MakeLocationMatchDto(returnString);
        }

        private static List<LocationMatchDto> MakeLocationMatchDto(JsonValue returnString)
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
                        EstablishmentId = establishment["EstablishmentID"],
                        Latitude = establishment["latitude"],
                        Longitude = establishment["longitude"]
                    };

                    restaurantMatches.Add(foundLocation);
                }
            }

            return restaurantMatches;
        }

        private static async Task<RestaurantInspectionDto> GetInspectionData(int establishmentId)
        {
            var findInspectionData =
                @"http://data.louisvilleky.gov/api/action/datastore/search.json?resource_id=db8b128e-aed3-4622-8059-1ffb74f652c6&filters[EstablishmentId]=" +
                establishmentId;

            var inspectionData = await HttpRequestSetup.MakeRequest(findInspectionData);

            return MakeRestaurantInspectionDto(inspectionData);
        }

        private static RestaurantInspectionDto MakeRestaurantInspectionDto(JsonValue returnString)
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
                        EstablishmentId = establishment["EstablishmentID"],
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

        private static string TruncateAddress(YelpListingDto restaurant)
        {
            var streetNumberPos = restaurant.Address.IndexOf(" ", StringComparison.Ordinal);
            return restaurant.Address.Substring(0,streetNumberPos);
        }

        private static decimal TruncateCoords(decimal coord)
        {
            return Math.Truncate(coord*100)/100;
        }

        
    }
}