using System;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Geolocation;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        double lat;
        double lng;

        public void UpdateGPSPosition()
        {
            Geolocator geo = new Geolocator();

            if (geo.LocationStatus == PositionStatus.Disabled || geo.LocationStatus == PositionStatus.NotAvailable)
                throw new Exception("Can't get GPS position");

            Geoposition pos = geo.GetGeopositionAsync().AsTask().Result;

            this.lat = pos.Coordinate.Point.Position.Latitude;
            this.lng = pos.Coordinate.Point.Position.Longitude;


        }
        public MainPage()
        {
            this.InitializeComponent();
            displayData = new ObservableCollection<WeatherListViewItem>();

            this.weatherProvider = new OpenWeatherMap();

            // Try to obtain GPS position, if it's not possible, fallback to fixed default city - Tokyo
            try
            {
                UpdateGPSPosition();
                this.weatherProvider.SetLatLng(lat, lng);
                this.weatherProvider.UseLatLng();
            }
            catch
            {
                this.cityName.Text = "Tokyo";
                this.weatherProvider.SetCity(cityName.Text);
            }

            refreshListBox();

            //Use GPS only during loading
            this.weatherProvider.UseCityName();

            this.listView.ItemsSource = displayData;
        }

        /// <summary>
        /// Refreshes list box, event on refresh button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            this.weatherProvider.SetCity(cityName.Text);

            refreshListBox();

        }

        private async void refreshListBox()
        {
            try
            {
                await this.weatherProvider.UpdateData();
            }
            catch (Exception e)
            {
                if (e.Message == "City not found!")
                {

                    var dialog = new MessageDialog("Change the city name...");

                    dialog.Title = e.Message;
                    dialog.Commands.Add(new UICommand { Label = "Exit", Id = 1 });

                    dialog.ShowAsync();

                    return;

                }
                else
                {

                    var dialog = new MessageDialog("Problem with retrieving data...");

                    dialog.Title = "Internet connection";
                    dialog.Commands.Add(new UICommand { Label = "Retry", Id = 0 });
                    dialog.Commands.Add(new UICommand { Label = "Exit", Id = 1 });

                    var res = await dialog.ShowAsync();

                    if ((int)res.Id == 1)
                    {
                        Application.Current.Exit();
                    }
                    else
                    {
                        refreshListBox();
                        return;
                    }
                }

            }

            this.cityName.Text = weatherProvider.GetCity();

            this.displayData.Clear();

            foreach (DayWeatherInfo day in await this.weatherProvider.GetWeather())
            {
                this.displayData.Add(new WeatherListViewItem() { DateBox = day.date.ToString("dd.MM.yyyy"), TempBox = "Temp (C): " + Convert.KelvinToCelsius(day.tempDay).ToString("F"), DescBox = day.weatherShortInfo, Icon = day.icon });
            }

        }

        /// <summary>
        /// Displays detailed weather info on double-tap or double-click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            int itemIndex = listView.SelectedIndex;

            this.Frame.Navigate(typeof(WeatherDetails), weatherProvider.GetWeather(itemIndex));
        }

        /// <summary>
        /// Used for data binding
        /// </summary>
        public class WeatherListViewItem
        {
            public string DateBox { get; set; }
            public string TempBox { get; set; }
            public string DescBox { get; set; }
            public string Icon { get; set; }
        }
    }
}
