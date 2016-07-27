using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DangerouslyDelicious.Dtos
{
    public class RestaurantInspectionDto
    {
        public int EstablishmentID { get; set; }
        public int InspectionID { get; set; }
        public string EstablishmentName { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string TypeDescription { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string InspectionDate { get; set; }
        public int Score { get; set; }
        public string Grade { get; set; }
        public string NameSearch { get; set; }
        public string Intersection { get; set; }
    }
}