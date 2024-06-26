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
using System.Windows.Shapes;
using WeatherApp.Model;
using WeatherApp.ViewModel;
using System.Windows.Threading;

namespace WeatherApp.View
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        public LoadingScreen()
        {
            InitializeComponent();
            StartLoadingTimer();
        }
        private void AnimatedGIF_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement gif = sender as MediaElement;
            if (gif != null)
            {
                gif.Position = new TimeSpan(0, 0, 1);
                gif.Play();
            }
        }

        private void StartLoadingTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop(); 

            WeatherWindow weatherWindow = new WeatherWindow();
            weatherWindow.Show();

            this.Close();
        }
    }
}
