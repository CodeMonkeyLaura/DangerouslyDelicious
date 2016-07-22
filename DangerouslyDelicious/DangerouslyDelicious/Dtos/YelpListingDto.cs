namespace DangerouslyDelicious.Dtos
{
    public class YelpListingDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool LocationClosed { get; set; }
        public decimal Rating { get; set; }
        public string RatingImage { get; set; }
        public int NumberReviews { get; set; }
        public string MobileUrl { get; set; }
    }
}