using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjWeatherApp.WeatherView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Weather3HoursViewCell : ViewCell
    {

        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(string), typeof(Weather3HoursViewCell), string.Empty);
        public static readonly BindableProperty WeatherProperty = BindableProperty.Create(nameof(Weather), typeof(string), typeof(Weather3HoursViewCell), string.Empty);
        public static readonly BindableProperty TempProperty = BindableProperty.Create(nameof(Temp), typeof(string), typeof(Weather3HoursViewCell), string.Empty);

        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public string Temp
        {
            get { return (string)GetValue(TempProperty); }
            set { SetValue(TempProperty, value); }
        }

        public string Weather
        {
            get { return (string)GetValue(WeatherProperty); }
            set { SetValue(WeatherProperty, value); }
        }

        public Weather3HoursViewCell()
        {
            InitializeComponent();

            lblTime.SetBinding(Label.TextProperty, new Binding(nameof(Time), source: this));
            lblTemp.SetBinding(Label.TextProperty, new Binding(nameof(Temp), source: this));
            lblWeather.SetBinding(Label.TextProperty, new Binding(nameof(Weather), source: this));
        }
    }
}