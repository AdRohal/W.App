using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WeatherApp.Model
{
    public class CityWeatherCondition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Temperature { get; set; }
        public bool HasPrecipitation { get; set; }
    }
}
