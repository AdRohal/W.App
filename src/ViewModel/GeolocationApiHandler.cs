using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class GeolocationApiHandler
    {
        private const string IP_API_URL = "http://ip-api.com/json/";

        public static async Task<Geolocation> GetGeolocation()
        {
            Geolocation geolocation = new Geolocation();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(IP_API_URL);
                string json = await response.Content.ReadAsStringAsync();
                geolocation = JsonConvert.DeserializeObject<Geolocation>(json);
            }

            return geolocation;
        }

    }
}
