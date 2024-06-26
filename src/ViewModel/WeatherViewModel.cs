using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;
        CityWeatherConditionStorage storage = new CityWeatherConditionSqliteStorage();

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<CityWeatherCondition> SearchHistory { get; set; }

        private Weather currentWeather;

        public Weather CurrentWeather
        {
            get { return currentWeather; }
            set
            {
                currentWeather = value;
                OnPropertyChanged("CurrentWeather");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                if (selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentWeather();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
            SearchHistory = new ObservableCollection<CityWeatherCondition>();
            GetSearchHistory();

            new Action(async () => InitializeWeatherWithCurrentGeolocation())();
            ResetHistoryCommand = new RelayCommand(ResetHistory);
        }


        private async void InitializeWeatherWithCurrentGeolocation()
{
    var currentGeolocation = await GeolocationApiHandler.GetGeolocation();
    var cities = await AccuWeatherApiHandler.GetCities(currentGeolocation.City);
    if (cities != null && cities.Any()) // Ensure the list is not null and has elements
    {
        SelectedCity = cities[0];
    }
    else
    {
        // This could involve setting SelectedCity to null, displaying a message to the user, etc.
        SelectedCity = null;
        // For example, you could log this situation or inform the user:
        Console.WriteLine("No cities found for the current geolocation.");
    }
}
        private async void GetCurrentWeather()
        {
            if (SelectedCity == null)
            {
                Console.WriteLine("SelectedCity is null.");
                return;
            }

            Query = string.Empty;
            CurrentWeather = await AccuWeatherApiHandler.GetCurrentWeather(SelectedCity.Key);
            AddCityWeatherConditionToHistory();
            Cities.Clear();
        }


        public async void MakeCitiesQuery()
        {
            var cities = await AccuWeatherApiHandler.GetCities(Query);

            Cities.Clear();

            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public void AddCityWeatherConditionToHistory()
        {
            CityWeatherCondition cityWeatherCondition = new CityWeatherCondition()
            {
                Name = selectedCity.LocalizedName,
                Temperature = currentWeather.Temperature.Metric.Value,
                HasPrecipitation = currentWeather.HasPrecipitation
            };

            storage.Insert(cityWeatherCondition);

            GetSearchHistory();
        }

        private void GetSearchHistory()
        {
            var searchHistory = storage.GetAll();

            SearchHistory.Clear();

            foreach (var searchHistoryItem in searchHistory)
            {
                SearchHistory.Add(searchHistoryItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand ResetHistoryCommand { get; private set; }

        private void ResetHistory()
        {
            
            storage.ResetHistory();
            SearchHistory.Clear(); 
        }

    }
}
