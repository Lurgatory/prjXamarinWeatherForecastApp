using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;

namespace prjWeatherApp.MapView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapContentView : ContentView
    {
        public MapContentView()
        {
            InitializeComponent();
        }

        private void Map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            Console.WriteLine("Map Clicked Latitude:" + e.Position.Latitude + " Longitude: " + e.Position.Longitude);
            Navigation.ShowPopup(new MapPopup(e.Position.Latitude,e.Position.Longitude));
        }
    }
}