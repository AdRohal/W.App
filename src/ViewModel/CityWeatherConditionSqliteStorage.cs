using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class CityWeatherConditionSqliteStorage : CityWeatherConditionStorage
    {
        private List<CityWeatherCondition> cityWeatherConditions;
        private string databasePath;

        public CityWeatherConditionSqliteStorage()
        {
            string databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string databaseName = "CityWeatherConditions.db";
            databasePath = System.IO.Path.Combine(databaseDirectory, databaseName);
        }

        public List<CityWeatherCondition> GetAll()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<CityWeatherCondition>();
                cityWeatherConditions = connection.Table<CityWeatherCondition>().ToList();
            }
            return cityWeatherConditions;
        }

        public void Insert(CityWeatherCondition cityWeatherCondition)
        {
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<CityWeatherCondition>();
                connection.Insert(cityWeatherCondition);
            }
        }

        public void ResetHistory()
        {
            using (var connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<CityWeatherCondition>();
                connection.DeleteAll<CityWeatherCondition>();
            }
        }
    }
}
