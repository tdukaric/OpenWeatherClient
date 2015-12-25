using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IWeatherProvider weatherProvider;

        ObservableCollection<WeatherListViewItem> displayData;
        public MainPage()
        {
            this.InitializeComponent();
            displayData = new ObservableCollection<WeatherListViewItem>();


            this.cityName.Text = "Tokyo";

            this.weatherProvider = new OpenWeatherMap();

            this.weatherProvider.SetCity(cityName.Text);
            refreshListBox();
            this.listView.ItemsSource = displayData;
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            this.weatherProvider.SetCity(cityName.Text);

            refreshListBox();

        }

        private async void refreshListBox()
        {
            await this.weatherProvider.UpdateData();

            this.cityName.Text = weatherProvider.GetCity();

            this.displayData.Clear();
            
            foreach(DayWeatherInfo day in await this.weatherProvider.GetWeather())
            {
                this.displayData.Add(new WeatherListViewItem() { DateBox = day.date.ToString("dd.MM.yyyy"), TempBox = Convert.KelvinToCelsius(day.tempDay).ToString("F"), DescBox = day.weatherShortInfo });

            }

                                                                   
        }

        private async void display_DoubleTapped_1(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            int itemIndex = listView.SelectedIndex;
                                                    
            this.Frame.Navigate(typeof(WeatherDetails), await weatherProvider.GetWeather(itemIndex));
                        
        }

        public class WeatherListViewItem
        {
            public string DateBox { get; set; }
            public string TempBox { get; set; }
            public string DescBox { get; set; }
        }
    }
}
