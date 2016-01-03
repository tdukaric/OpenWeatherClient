using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class WeatherDetails : Page
    {

        DayWeatherInfo info;
        public WeatherDetails()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            info = e.Parameter as DayWeatherInfo;

            this.gridListViewItem.DataContext = info;/*

            this.dateTest.Text = info.date.ToString("dd.MM.yyyy");
            this.tempMorning.Text = info.tempMorning.ToString("F");
            this.tempDay.Text = info.tempDay.ToString("F");
            this.tempEvening.Text = info.tempEvening.ToString("F");
            this.windSpeed.Text = info.windSpeed.ToString();
            this.humidity.Text = info.humidity.ToString();
            this.pressure.Text = info.pressure.ToString();
            this.rain.Text = info.rain.ToString();
            this.weatherShortInfo.Text = info.weatherShortInfo;*/

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null && this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        #if WINDOWS_PHONE_APP
        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
          if (this.Frame != null && this.Frame.CanGoBack)
          {
            e.Handled = true;
            this.Frame.GoBack();
          }
        }
        #endif
    }
}
