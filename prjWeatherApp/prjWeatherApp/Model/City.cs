using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace prjWeatherApp.Model
{
    public class City
    {
        [PrimaryKey]
        public string CityName { get; set; }
    }
}
