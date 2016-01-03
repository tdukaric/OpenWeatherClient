using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

            DataContext = new SettingsPageViewModel(Settings.GetTemperatureUnitFromSettings());

            SettingsTemperatureUnitRadioCheck();

            displayData = new ObservableCollection<WeatherListViewItem>();

            this.weatherProvider = new OpenWeatherMap(((SettingsPageViewModel)DataContext).SelectedTemperatureUnit);

            // Try to obtain GPS position, if it's not possible, fallback to fixed default city - Tokyo
            try
            {
                KeyValuePair<double, double> latlng = GetGPSPosition();
                this.weatherProvider.SetLatLng(latlng.Key, latlng.Value);
                this.weatherProvider.UseLatLng();
            }
            catch
            {
                this.cityName.Text = "Tokyo";
                this.weatherProvider.SetCity(cityName.Text);
            }

            UpdateAndRefreshListBox();

            //Use GPS only during loading
            this.weatherProvider.UseCityName();

            this.listView.ItemsSource = displayData;
        }
        
        private void SettingsTemperatureUnitRadioCheck()
        {
            if (((SettingsPageViewModel)DataContext).SelectedTemperatureUnit.Equals("Celsius"))
            {
                this.Celsius.IsChecked = true;
            }
            else if (((SettingsPageViewModel)DataContext).SelectedTemperatureUnit.Equals("Kelvin"))
            {
                this.Kelvin.IsChecked = true;
            }
            else
            {
                this.Fahrenheit.IsChecked = true;
            }
        }

        /// <summary>
        /// Updates temperature unit and saves it to the local settings.
        /// </summary>
        public void SetTemperatureUnit()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            var value = localSettings.Values["temperatureUnit"];

            if (value == null)
            {
                value = TemperatureUnit.Celsius;
            }
            else
            {
                if (!(value is TemperatureUnit))
                    value = TemperatureUnit.Celsius;
            }

            this.weatherProvider.SetTemperatureUnit(((SettingsPageViewModel)DataContext).SelectedTemperatureUnit);

            Settings.SaveTemperatureUnitToSettings(((SettingsPageViewModel)DataContext).SelectedTemperatureUnit);
        }

        private async void UpdateAndRefreshListBox()
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

                    await dialog.ShowAsync();

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
                        UpdateAndRefreshListBox();
                        return;
                    }
                }

            }

            this.cityName.Text = weatherProvider.GetCity();

            await RefreshListBox();

        }

        private async Task RefreshListBox()
        {
            this.displayData.Clear();

            foreach (DayWeatherInfo day in await this.weatherProvider.GetWeather())
            {
                this.displayData.Add(new WeatherListViewItem() { DateBox = day.date.ToString("dd.MM.yyyy"), TempBox = "Temp (C): " + day.tempDay.ToString("F"), DescBox = day.weatherShortInfo, Icon = day.icon });
            }
        }


        /// <summary>
        /// Returns GPS position.
        /// </summary>
        /// <returns>KeyValuePair where key is latitude and value is longitude.</returns>
        public KeyValuePair<double, double> GetGPSPosition()
        {
            Geolocator geo = new Geolocator();

            if (geo.LocationStatus == PositionStatus.Disabled || geo.LocationStatus == PositionStatus.NotAvailable)
                throw new Exception("Can't get GPS position");

            Geoposition pos = geo.GetGeopositionAsync().AsTask().Result;

            return new KeyValuePair<double, double>(pos.Coordinate.Point.Position.Latitude, pos.Coordinate.Point.Position.Longitude);

        }

    }
    
}
