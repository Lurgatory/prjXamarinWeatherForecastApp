using prjWeatherApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using static prjWeatherApp.Model.WeatherClass;

namespace prjWeatherApp.MapView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPopup : Popup
    {
        public MapPopup(double lat, double lon)
        {

            InitializeComponent();
            _ = GetWeather(lat.ToString("0.000"), lon.ToString("0.000"));
        }

        public async Task GetWeather(string lat, string lon)
        {
            var weatherVM = new WeatherViewModel();
            var body = await weatherVM.GetWeatherByLatLon(lat, lon);
            var weatherData = JsonConvert.DeserializeObject<MainWeatherData>(body);
            cityName.Text = weatherData.Name;
            highestTempLabel.Text = weatherVM.KelvinToFahrenheit(weatherData.Main.Temp_max) + "°F";
            lowestTempLabel.Text = weatherVM.KelvinToFahrenheit( weatherData.Main.Temp_min) + "°F";
            humidityLabel.Text = weatherData.Main.Humidity.ToString() + "%";
            windLevelLabel.Text = weatherData.Wind.Speed.ToString() + " mph";
            feelsLikeLabel.Text = weatherVM.KelvinToFahrenheit(weatherData.Main.Feels_like) + "°F";


        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}