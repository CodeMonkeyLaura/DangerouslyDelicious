namespace DangerouslyDelicious.Dtos
{
    public class LocationMatchDto
    {
        public string Name { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public int EstablishmentId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}