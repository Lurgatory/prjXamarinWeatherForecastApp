using prjWeatherApp.ListLocationView;
using prjWeatherApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using prjWeatherApp.ViewModel;
using prjWeatherApp.WeatherView;

namespace prjWeatherApp.ListView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListContentView : ContentView
    {

        public ListContentView()
        {
            InitializeComponent();

            RefreshListView();

            foreach (var cell in lstViewCities.TemplatedItems)
            {
                if (cell is  ListWeatherViewCell weatherCell)
                {
                    weatherCell.DeleteClicked += WeatherCell_DeleteClicked;
                }
            }
            
        }

        private void WeatherCell_DeleteClicked(object sender, EventArgs e)
        {
            RefreshListView();
        }
        public void RefreshListView()
        {
            App.DatabaseLocation = "/data/user/0/com.companyname.prjweatherapp/files/weatherDb.sqlite";
            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                con.CreateTable<City>();
                var cities = con.Table<City>().ToList();
                lstViewCities.ItemsSource = cities;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListSearchPage());
        }

        private void RefreshListView_Tapped(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void lstViewCities_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var selectedCity = e.SelectedItem as City;
            if (selectedCity != null)
            {
                string cityName = selectedCity.CityName;
                Navigation.PushAsync(new ListItemPage(cityName));
                lstViewCities.SelectedItem = null;
            }
            
        }
    }
}