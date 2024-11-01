namespace WeatherApp.Models
{
    public class User
    {
        public long User_ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password_hs { get; set; } = string.Empty;
        public long? Favourite_location_ID { get; set; }
    }
}