using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Travel_Manager
{
    public class Weather
    {
        private static readonly string WEATHER_URL = "http://api.weatherapi.com/v1/current.json";
        private static readonly string API_KEY = "378baee090cd416e8b5182934241410";
        public async Task<List<string>> GetCurrentWeather(string location)
        {
            var client = new RestClient(WEATHER_URL);
            var request = new RestRequest();
            request.AddParameter("key", API_KEY);
            request.AddParameter("q", location);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var weatherResult = JsonSerializer.Deserialize<WeatherResponse>(response.Content);

                
                var weatherDetails = new List<string>
                {
                    $"Location: {weatherResult.location.name}, {weatherResult.location.region}, {weatherResult.location.country}",
                    $"Temperature (C): {weatherResult.current.temp_c}°C",
                    $"Temperature (F): {weatherResult.current.temp_f}°F",
                    $"Condition: {weatherResult.current.condition.text}",
                    $"Humidity: {weatherResult.current.humidity}%",
                    $"Wind: {weatherResult.current.wind_mph} mph ({weatherResult.current.wind_kph} kph) {weatherResult.current.wind_dir}",
                    $"Pressure: {weatherResult.current.pressure_mb} mb ({weatherResult.current.pressure_in} in)",
                    $"Precipitation: {weatherResult.current.precip_mm} mm ({weatherResult.current.precip_in} in)",
                    $"Feels Like (C): {weatherResult.current.feelslike_c}°C",
                    $"Feels Like (F): {weatherResult.current.feelslike_f}°F",
                    $"Visibility: {weatherResult.current.vis_km} km ({weatherResult.current.vis_miles} miles)",
                    $"UV Index: {weatherResult.current.uv}",
                    $"Gusts: {weatherResult.current.gust_mph} mph ({weatherResult.current.gust_kph} kph)",
                    $"Last Updated: {weatherResult.current.last_updated}"
                };

                return weatherDetails;
            }
            else
            {
                // If the API call fails
                return new List<string> { $"Failed to fetch weather: {response.StatusDescription}" };
            }
        }


        public class WeatherResponse
        {
            public Location location { get; set; }
            public Current current { get; set; }
        }


        public class Location
        {
            public string name { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public float lat { get; set; }
            public float lon { get; set; }
            public string tz_id { get; set; }
            public int localtime_epoch { get; set; }
            public string localtime { get; set; }
        }

        public class Current
        {
            public int last_updated_epoch { get; set; }
            public string last_updated { get; set; }
            public float temp_c { get; set; }
            public float temp_f { get; set; }
            public int is_day { get; set; }
            public Condition condition { get; set; }
            public float wind_mph { get; set; }
            public float wind_kph { get; set; }
            public int wind_degree { get; set; }
            public string wind_dir { get; set; }
            public float pressure_mb { get; set; }
            public float pressure_in { get; set; }
            public float precip_mm { get; set; }
            public float precip_in { get; set; }
            public int humidity { get; set; }
            public int cloud { get; set; }
            public float feelslike_c { get; set; }
            public float feelslike_f { get; set; }
            public float windchill_c { get; set; }
            public float windchill_f { get; set; }
            public float heatindex_c { get; set; }
            public float heatindex_f { get; set; }
            public float dewpoint_c { get; set; }
            public float dewpoint_f { get; set; }
            public float vis_km { get; set; }
            public float vis_miles { get; set; }
            public float uv { get; set; }
            public float gust_mph { get; set; }
            public float gust_kph { get; set; }
        }

        public class Condition
        {
            public string text { get; set; }
            public string icon { get; set; }
            public int code { get; set; }
        }

    }
}
