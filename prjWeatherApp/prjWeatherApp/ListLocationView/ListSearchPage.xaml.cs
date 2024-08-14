using prjWeatherApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using prjWeatherApp.ListView;
using prjWeatherApp.ListLocationView;

namespace prjWeatherApp.ListLocationView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListSearchPage : ContentPage
    {
        private readonly string[] sourceItems = new string[]
        {
            "Toronto",
            "Montreal",
            "Vancouver",
            "Calgary",
            "Edmonton",
            "Ottawa",
            "Winnipeg",
            "Hamilton",
            "Kitchener",
            "London",
            "Halifax",
            "Oshawa",
            "Victoria",
            "Windsor",
            "Saskatoon",
            "Regina",
            "Sherbrooke",
            "Barrie",
            "Kelowna",
            "Abbotsford",
            "Kingston",
            "Saguenay",
            "Trois-Rivières",
            "Guelph",
            "Moncton",
            "Brantford",
            "Thunder Bay"
        };

        public ObservableCollection<string> MyItems { get; set; }

        public EventHandler CityAdd;
        public ListSearchPage()
        {
            InitializeComponent();
            MyItems = new ObservableCollection<string>(sourceItems);
            cView.ItemsSource = MyItems;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            cView.IsVisible = true;
            var searchTerm = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLowerInvariant();

            var filteredItems = sourceItems.Where(x => x.ToLowerInvariant().Contains(searchTerm)).ToList();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                filteredItems = sourceItems.ToList();
            }

            foreach (var value in sourceItems)
            {
                if (!filteredItems.Contains(value))
                {
                    MyItems.Remove(value);
                }
                else if (!MyItems.Contains(value))
                {
                    MyItems.Add(value);
                }
            }
        }

        private void CollectViewLabelTapped(Object sender, EventArgs e)
        {
            var label = (Label)sender;
            var tappedItem = label.Text;
            City newCity = new City()
            {
                CityName = tappedItem
            };

            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {
                var existingCity = con.Table<City>().Where(c => c.CityName == newCity.CityName).FirstOrDefault();
                var cities = con.Table<City>().ToList();
                if (existingCity != null)
                {
                    DisplayAlert("Duplicate", "City " + existingCity.CityName + " already exists", "OK");
                    return;
                }

                con.CreateTable<City>();
                int row = con.Insert(newCity);
                
                if (row > 0)
                {
                    DisplayAlert("Success", "City added", "Ok");
                    
                }
                else
                {
                    DisplayAlert("Failure", "City not added", "Ok");
                }

                CityAdd?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}