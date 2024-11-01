using System.Text.Json;

namespace WeatherApp.Classes
{
    public static class WeatherDataFetcher
    {
        private static readonly string API_KEY = "eb749d90c61648de9d181233243010";

        public static async Task<WeatherData> FetchWeatherData(int id, ushort days)
        {
            WeatherData weatherData = new();
            string rawData = "";

            using (HttpClient client = new())
            {
                rawData = await client.GetStringAsync($"http://api.weatherapi.com/v1/forecast.json?key={API_KEY}&q=id:{id}&days={days}&aqi=no&alerts=no");
            }

            weatherData = JsonSerializer.Deserialize<WeatherData>(rawData)!;

            return weatherData;
        }
    }
}
