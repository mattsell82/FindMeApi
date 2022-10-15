using FindMeApi.Data;

namespace FindMeApi.Models
{
    public class Location
    {
        public int Id { get; set; }
        public double? Accuracy { get; set; }
        public bool? ReducedAccuracy { get; set; }
        public double? VerticalAccuracy { get; set; }
        public double? Altitude { get; set; }
        public double? Course { get; set; }
        public bool? IsFromMockProvider { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }




}

