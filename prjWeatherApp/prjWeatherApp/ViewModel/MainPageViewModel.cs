using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace prjWeatherApp.ViewModel
{
    public class MainPageViewModel: BindableObject
    {
        private bool isWeatherViewVisible;
        private bool isMapViewVisible;
        private bool isListViewVisible;

        public ICommand WeatherCommand { get; set; }
        public ICommand MapCommand { get; set; }
        public ICommand ListCommand { get; set; }

        public ICommand ViewCommand { get; set; }

        public bool IsWeatherViewVisible
        {
            get { return isWeatherViewVisible; }
            set
            {
                isWeatherViewVisible = value;
                OnPropertyChanged(nameof(IsWeatherViewVisible));
                
            }
        }
        
        public bool IsMapViewVisible
        {
            get { return isMapViewVisible; }
            set
            {
                isMapViewVisible = value;
                OnPropertyChanged(nameof(IsMapViewVisible));
               
            }
        }

        public bool IsListViewVisible
        {
            get { return isListViewVisible; }
            set
            {
                isListViewVisible = value;
                OnPropertyChanged(nameof(IsListViewVisible));
                
            }
        }

        public MainPageViewModel()
        {
            WeatherCommand = new Command(WeatherCommandExecute);
            MapCommand = new Command(MapCommandExecute);
            ListCommand = new Command(ListCommandExecute);
        }

        private void WeatherCommandExecute(object obj)
        {
            IsWeatherViewVisible = true;
            IsMapViewVisible = false;
            IsListViewVisible = false;
        }

        public void MapCommandExecute(object obj)
        {
            IsWeatherViewVisible = false;
            IsMapViewVisible = true;
            IsListViewVisible = false;
            
        }

        public void ListCommandExecute(object obj)
        {
            IsWeatherViewVisible = false;
            IsMapViewVisible = false;
            IsListViewVisible = true;
            
        }
    }
}
