namespace DangerouslyDelicious.Dtos
{
    public class RestaurantInspectionDto
    {
        public int EstablishmentId { get; set; }
        public int InspectionId { get; set; }
        public string EstablishmentName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string InspectionDate { get; set; }
        public int Score { get; set; }
        public string Grade { get; set; }
    }
}