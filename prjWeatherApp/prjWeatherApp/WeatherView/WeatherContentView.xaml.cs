using Newtonsoft.Json.Linq;
using prjWeatherApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using static prjWeatherApp.Model.WeatherClass;
using static prjWeatherApp.Model.WeatherForecast;
using System.Runtime.CompilerServices;
using prjWeatherApp.WeatherView;
using static System.Net.WebRequestMethods;
using prjWeatherApp.ViewModel;


namespace prjWeatherApp.WeatherView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherContentView : ContentView
    {
        private static string defaultCity = "montreal";

        double lat, lon;
        public static readonly BindableProperty CityNameProperty = BindableProperty.Create(nameof(CityName), typeof(string), typeof(WeatherContentView), string.Empty, propertyChanged: OnCityNameChanged);

        private static async void  OnCityNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && newValue != oldValue)
            {
                var cell = (WeatherContentView)bindable;
                await cell.InitializeAsync(newValue.ToString());
            }
        }

        public string CityName
        {
            get { return (string)GetValue(CityNameProperty); }
            set { SetValue(CityNameProperty, value); }
        }

        public WeatherContentView()
        {
            InitializeComponent();

            lblCountry.SetBinding(Label.TextProperty, new Binding(nameof(CityName), source: this));
            var text = CityNameProperty;
            _ = InitializeAsync(defaultCity);
        }

        public async Task InitializeAsync(string cityName)
        {
            await GetWeather(cityName);
            await GetThreeHourForecast(lat.ToString(), lon.ToString());
        }

        private async Task GetWeather(string cityName)
        {
            var weatherVM = new WeatherViewModel();
            var body = await weatherVM.GetWeather(cityName);
            var weatherData = JsonConvert.DeserializeObject<MainWeatherData>(body);
            lblCountry.Text = weatherData.Name;
            lblTemp.Text = (int)weatherData.Main.Temp + "°";
            lblDescription.Text = weatherData.Weather.FirstOrDefault()?.Main + "   Min: " + (int)weatherData.Main.Temp_min + "°/Max: " + (int)weatherData.Main.Temp_max + "°";
            weatherAirHumidity.WeatherSubDetailValue = weatherData.Main.Humidity.ToString() + "%";
            weatherWind.WeatherSubDetailValue = weatherData.Wind.Speed.ToString() + " km/h";
            weatherPressure.WeatherSubDetailValue = weatherData.Main.Pressure.ToString() + " hPa";
            weatherCloudness.WeatherSubDetailValue = weatherData.Clouds.All.ToString() + "%";
            lat = weatherData.Coord.Lat;
            lon = weatherData.Coord.Lon;
        }

        public async Task GetThreeHourForecast(string lat, string lon)
        {
            var weatherVM = new WeatherViewModel();
            var body = await weatherVM.Get3HourForecast(lat,lon);
            var weatherForecastData = JsonConvert.DeserializeObject<Root>(body);

            List<ThreeHoursForecast> threeHoursForecasts = weatherForecastData.List.Select(x => new ThreeHoursForecast
            {
                Dt_txt = DateTime.Parse(x.Dt_txt).ToString("HH:mm"),
                Weather = x.Weather.FirstOrDefault()?.Main,
                Temp = weatherVM.KelvinToFahrenheit(x.Main.Temp) + "°"
            }).ToList();
            listView.ItemsSource = threeHoursForecasts;
        }

        private void OnMoreDetailTapped(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new WeatherDetailPage(lblCountry.Text));
        }
    }
}
