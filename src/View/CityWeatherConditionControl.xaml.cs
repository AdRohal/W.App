using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherApp.Model;

namespace WeatherApp.View
{
    public partial class CityWeatherConditionControl : UserControl
    {
        public CityWeatherCondition CityWeatherCondition
        {
            get { return (CityWeatherCondition)GetValue(CityWeatherConditionProperty); }
            set { SetValue(CityWeatherConditionProperty, value); }
        }

        public static readonly DependencyProperty CityWeatherConditionProperty =
            DependencyProperty.Register("CityWeatherCondition", typeof(CityWeatherCondition), typeof(CityWeatherConditionControl), new PropertyMetadata(new CityWeatherCondition { Name = "Name", Temperature = "30", HasPrecipitation = true }, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CityWeatherConditionControl control = d as CityWeatherConditionControl;
            if (control != null)
            {
                control.nameTextBlock.Text = (e.NewValue as CityWeatherCondition).Name + ": ";
                control.temperaturTextBlock.Text = (e.NewValue as CityWeatherCondition).Temperature + "°C";
            }
        }

        public CityWeatherConditionControl()
        {
            InitializeComponent();
        }
    }
}
