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
    public class AccuWeatherApiHandler
    {
        private const string ACCU_WEATHER_URL = "http://dataservice.accuweather.com/";
        private const string CITY_AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        private const string CURRENT_WEATHER_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        private const string API_KEY = "QmLZ08cLQlQao8DdUKX4oKbaKLe7gre5";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            string url = ACCU_WEATHER_URL + string.Format(CITY_AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }


        public static async Task<Weather> GetCurrentWeather(string cityKey)
        {
            Weather currentWeather = new Weather();

            string url = ACCU_WEATHER_URL + string.Format(CURRENT_WEATHER_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                currentWeather = (JsonConvert.DeserializeObject<List<Weather>>(json)).FirstOrDefault();
            }

            return currentWeather;
        }
    }
}
