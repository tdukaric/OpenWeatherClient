using System.ComponentModel;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// View model used for Settings Flyover
    /// </summary>
    class SettingsPageViewModel : INotifyPropertyChanged
    {
        public SettingsPageViewModel(TemperatureUnit initValue)
        {
            _selectedTemperatureUnit = initValue;
        }
        public SettingsPageViewModel()
        {
            _selectedTemperatureUnit = TemperatureUnit.Celsius;
        }

        private TemperatureUnit _selectedTemperatureUnit;

        public TemperatureUnit SelectedTemperatureUnit
        {
            get
            {
                return _selectedTemperatureUnit;
            }
            set
            {

                if (_selectedTemperatureUnit != value)
                {
                    _selectedTemperatureUnit = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedTemperatureUnit"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
