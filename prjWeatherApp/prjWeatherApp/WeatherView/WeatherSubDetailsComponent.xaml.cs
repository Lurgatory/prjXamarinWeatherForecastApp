using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjWeatherApp.WeatherView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherSubDetailsComponent : ContentView
    {
        public static readonly BindableProperty WeatherSubDetailIconProperty = BindableProperty.Create(nameof(WeatherSubDetailIcon), typeof(string), typeof(WeatherSubDetailsComponent), string.Empty);
        public static readonly BindableProperty WeatherSubDetailTitleProperty = BindableProperty.Create(nameof(WeatherSubDetailTitle), typeof(string), typeof(WeatherSubDetailsComponent), string.Empty);
        public static readonly BindableProperty WeatherSubDetailValueProperty = BindableProperty.Create(nameof(WeatherSubDetailValue), typeof(string), typeof(WeatherSubDetailsComponent), string.Empty);
        public string WeatherSubDetailIcon
        {
            get { return (string)GetValue(WeatherSubDetailIconProperty); }
            set { SetValue(WeatherSubDetailIconProperty, value); }
        }

        public string WeatherSubDetailTitle
        {
            get { return (string)GetValue(WeatherSubDetailTitleProperty); }
            set { SetValue(WeatherSubDetailTitleProperty, value); }
        }

        public string WeatherSubDetailValue
        {
            get { return (string)GetValue(WeatherSubDetailValueProperty); }
            set { SetValue(WeatherSubDetailValueProperty, value); }
        }

        public WeatherSubDetailsComponent()
        {
            InitializeComponent();

            imgWeatherSubDetailIcon.SetBinding(Image.SourceProperty, new Binding(nameof(WeatherSubDetailIcon), source: this));
            lblWeatherSubDetailTitle.SetBinding(Label.TextProperty, new Binding(nameof(WeatherSubDetailTitle), source: this));
            lblWeatherSubDetailValue.SetBinding(Label.TextProperty, new Binding(nameof(WeatherSubDetailValue), source: this));
        }
    }
}