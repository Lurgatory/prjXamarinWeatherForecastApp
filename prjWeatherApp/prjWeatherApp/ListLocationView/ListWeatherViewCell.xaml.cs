using prjWeatherApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using static prjWeatherApp.Model.WeatherClass;
using prjWeatherApp.Model;
using SQLite;
using System.Diagnostics;
using prjWeatherApp.ListView;

namespace prjWeatherApp.ListLocationView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListWeatherViewCell : ViewCell
	{

		public static readonly BindableProperty CityNameProperty = BindableProperty.Create(nameof(CityName),typeof(string),typeof(ListWeatherViewCell), propertyChanged: OnCityNameChanged);

        public string CityName
		{
            get { return (string)GetValue(CityNameProperty); }
            set { SetValue(CityNameProperty, value); }
        }

		public event EventHandler DeleteClicked;

		public ListWeatherViewCell ()
		{
			InitializeComponent ();
			lblCityName.SetBinding(Label.TextProperty, new Binding(nameof(CityName), source: this));
		}

		private static async void OnCityNameChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (newValue != null && newValue != oldValue)
			{
				var cell = (ListWeatherViewCell)bindable;
				await cell.GetWeather(newValue.ToString());
			}
        }

        public async Task GetWeather(string inputCityName)
		{
			var weatherVM = new WeatherViewModel();
			var body = await weatherVM.GetWeather(inputCityName);
			var weatherData = JsonConvert.DeserializeObject<MainWeatherData>(body);
			lblHumidity.Text = "Humidity: " + weatherData.Main.Humidity.ToString() + "%";
			lblTemp.Text = (int)weatherData.Main.Temp + "°F";
			lblWeatherMain.Text = weatherData.Weather.FirstOrDefault()?.Main;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
			var menuitem = (MenuItem)sender;
			var oneCity = (City)menuitem.CommandParameter;
			using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
			{
                con.CreateTable<City>();
                con.Delete(oneCity);
            }
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}