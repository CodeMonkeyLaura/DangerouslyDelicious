using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DangerouslyDelicious.Services;

namespace DangerouslyDelicious.Tests.IntegrationTests.ServicesTests
{
    [TestClass]
    public class RestaurantInspectionServiceTests
    {
        //[TestMethod]
        //public void ItMustGetARestaurantInspectionDtoObjectByRestaurantName()
        //{
        //    var name = "Royals Chicken";
        //    var restaurantInspectionInfo = RestaurantInspectionData.GetRestaurantInspectionInfoByName(name);
        //    Assert.IsNotNull(restaurantInspectionInfo);
        //    Assert.IsTrue(name.Equals(restaurantInspectionInfo.EstablishmentName, StringComparison.InvariantCultureIgnoreCase));
        //}

        //[TestMethod]
        //public void ItMustReturnCleanJsonString()
        //{
        //    var cleanJson = RestaurantInspectionData.CleanJsonString("([{\"EstablishmentId\":98995,\"InspectionId\":1171636,\"EstablishmentName\":\"ROYALS CHICKEN\",\"PlaceName\":\" \",\"Address\":\"736 E.MARKET\",\"Address2\":\"\",\"City\":\"LOUISVILLE\",\"State\":\"KY\",\"Zip\":\"40202\",\"TypeDescription\":\"FOOD SERVICE\",\"Latitude\":38.2531000,\"Longitude\":-85.7386000,\"InspectionDate\":\"2 / 10 / 2016\",\"Score\":100,\"Grade\":\"A\",\"NameSearch\":\"ROYALSCHICKEN\",\"Intersection\":null}] );");
        //    Assert.AreEqual("{\"EstablishmentId\":98995,\"InspectionId\":1171636,\"EstablishmentName\":\"ROYALS CHICKEN\",\"PlaceName\":\" \",\"Address\":\"736 E.MARKET\",\"Address2\":\"\",\"City\":\"LOUISVILLE\",\"State\":\"KY\",\"Zip\":\"40202\",\"TypeDescription\":\"FOOD SERVICE\",\"Latitude\":38.2531000,\"Longitude\":-85.7386000,\"InspectionDate\":\"2 / 10 / 2016\",\"Score\":100,\"Grade\":\"A\",\"NameSearch\":\"ROYALSCHICKEN\",\"Intersection\":null}", cleanJson);
        //}
    }
}
