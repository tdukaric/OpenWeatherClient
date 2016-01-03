
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Refreshes list box, event on refresh button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            this.weatherProvider.SetCity(cityName.Text);

            UpdateAndRefreshListBox();

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

        private async void saveSettings_Click(object sender, RoutedEventArgs e)
        {
            SetTemperatureUnit();

            foreach (DayWeatherInfo day in this.weatherProvider.GetWeather().Result)
            {
                this.displayData.Add(new WeatherListViewItem() { DateBox = day.date.ToString("dd.MM.yyyy"), TempBox = "Temp (C): " + day.tempDay.ToString("F"), DescBox = day.weatherShortInfo, Icon = day.icon });
            }

            await RefreshListBox();

            this.SettingsFlyout.Hide();
        }
    }
}