using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static prjWeatherApp.Model.WeatherClass;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prjWeatherApp.ViewModel
{
    class WeatherViewModel
    {
        public async Task<string> GetWeather(string cityName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/" + cityName + "/EN"),
                Headers =
                {
                    { "X-RapidAPI-Key", "555c6999bcmsh49422eb2601af5ap171c90jsnfbc692fdc354" },
                    { "X-RapidAPI-Host", "open-weather13.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public async Task<string> Get3HourForecast(string lat,string lon)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/fivedaysforcast/" + lat + "/" + lon),
                Headers =
                {
                    { "X-RapidAPI-Key", "555c6999bcmsh49422eb2601af5ap171c90jsnfbc692fdc354" },
                    { "X-RapidAPI-Host", "open-weather13.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public async Task<string> GetWeatherByLatLon(string lat, string lon)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/latlon/" + lat + "/" + lon),
                Headers =
                {
                    { "X-RapidAPI-Key", "555c6999bcmsh49422eb2601af5ap171c90jsnfbc692fdc354" },
                    { "X-RapidAPI-Host", "open-weather13.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public string KelvinToFahrenheit(double kelvin)
        {
            string fahrenheit = ((int)((kelvin - 273.15) * 1.8 + 32)).ToString();
            return fahrenheit;
        }
    }
}
