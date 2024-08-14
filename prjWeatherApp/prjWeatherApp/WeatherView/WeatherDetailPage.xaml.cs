using prjWeatherApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using static prjWeatherApp.Model.WeatherClass;

namespace prjWeatherApp.WeatherView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherDetailPage : ContentPage
    {
        public WeatherDetailPage(string cityName)
        {
            InitializeComponent();
            _ = GetCityWeather(cityName);
        }

        public async Task GetCityWeather(string cityName)
        {
            var weatherVM = new WeatherViewModel();
            var body = await weatherVM.GetWeather(cityName);
            var weatherData = JsonConvert.DeserializeObject<MainWeatherData>(body);
            cityLabel.Text = weatherData.Name;
            highestTempLabel.Text = (int)weatherData.Main.Temp_max + "°F";
            lowestTempLabel.Text = (int)weatherData.Main.Temp_min + "°F";
            humidityLabel.Text = weatherData.Main.Humidity + "%";
            windLevelLabel.Text = weatherData.Wind.Speed + " km/h";
            feelsLikeLabel.Text = (int)weatherData.Main.Feels_like + "°F";
        }
    }
}