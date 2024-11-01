using System.Text.Json;

namespace WeatherApp.Classes
{
    public static class LocationOptionFetcher
    {
        private static readonly string API_KEY = "ac6bad8fb05a4999bb680317240910";

        public static async Task<List<LocationOption>?> FetchLocationOptions(string query)
        {
            HttpClient client = new();

            string locationQueryResults = await client.GetStringAsync($@"http://api.weatherapi.com/v1/search.json?key={API_KEY}&q={query}");

            client.Dispose();

            List<LocationOption>? locations = JsonSerializer.Deserialize<List<LocationOption>>(locationQueryResults);

            return locations;
        }

        public static Dictionary<string, LocationOption> FetchNameAndRegion(List<LocationOption>? locationOptions)
        {
            Dictionary<string, LocationOption> locs = new();

            foreach (var loc in locationOptions!)
            {
                locs[$"{loc.Name}, {loc.Region}, {loc.Country}"] = loc;
            }

            return locs;
        }
    }
}
