using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Model
{
    public class CitiesResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<City> Cities { get; set; }
    }

}
