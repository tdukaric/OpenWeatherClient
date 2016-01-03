using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OpenWeatherClientInfoteria
{
    class Settings
    {
        static public TemperatureUnit GetTemperatureUnitFromSettings()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            var value = localSettings.Values["temperatureUnit"];

            if (value == null)
            {
                return TemperatureUnit.Celsius;
            }
            else
            {
                if (value.Equals("Celsius"))
                {
                    return TemperatureUnit.Celsius;
                }
                else if (value.Equals("Kelvin"))
                {
                    return TemperatureUnit.Kelvin;
                }
                else
                {
                    return TemperatureUnit.Fahrenheit;
                }
            }
        }


        static public void SaveTemperatureUnitToSettings(TemperatureUnit unit)
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            localSettings.Values["temperatureUnit"] = unit.ToString();

        }
    }
}
