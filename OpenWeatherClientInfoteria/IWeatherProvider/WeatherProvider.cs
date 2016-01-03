using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// Abstract class which implements IWeatherProvider interface
    /// </summary>
    public abstract class WeatherProvider : IWeatherProvider
    {
        protected string city;
        protected string country;
        protected double lat;
        protected double lng;
        protected bool useLatLng = false;

        public TemperatureUnit temperatureUnit;

        public void SetCity(string City)
        {
            this.city = City;
        }

        public void SetCountry(string Country)
        {
            this.country = Country;
        }

        protected DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public string GetCity()
        {
            return city;
        }

        public void SetLatLng(double Lat, double Lng)
        {
            this.lat = Lat;
            this.lng = Lng;
        }

        public bool UseLatLng()
        {
            this.useLatLng = true;
            return this.useLatLng;
        }

        public bool UseCityName()
        {
            this.useLatLng = false;
            return this.useLatLng;
        }

        public abstract Task UpdateData();

        public abstract Task<DayWeatherInfo> GetWeather(DateTime day);

        public abstract DayWeatherInfo GetWeather(int index);

        public abstract Task<List<DayWeatherInfo>> GetWeather();

        public abstract void SetTemperatureUnit(TemperatureUnit target);

        protected double ChangeTemperatureUnit(double value, TemperatureUnit source, TemperatureUnit target)
        {
            if (source == target)
                return value;
            else if (source == TemperatureUnit.Celsius)
            {
                if (target == TemperatureUnit.Kelvin)
                {
                    return TemperatureUnitConvert.CelsiusToKelvin(value);
                }
                if (target == TemperatureUnit.Fahrenheit)
                {
                    return TemperatureUnitConvert.CelsiusToFahrenheit(value);
                }
            }
            else if (source == TemperatureUnit.Kelvin)
            {
                if (target == TemperatureUnit.Celsius)
                {
                    return TemperatureUnitConvert.KelvinToCelsius(value);
                }
                if (target == TemperatureUnit.Fahrenheit)
                {
                    return TemperatureUnitConvert.KelvinToFahrenheit(value);
                }
            }
            else if (source == TemperatureUnit.Fahrenheit)
            {
                if (target == TemperatureUnit.Celsius)
                {
                    return TemperatureUnitConvert.FahrenheitToCelsius(value);
                }
                if (target == TemperatureUnit.Kelvin)
                {
                    return TemperatureUnitConvert.FahrenheitToKelvin(value);
                }
            }

            throw new Exception("Temperature Unit Not Found!!!");

        }
    }
}
