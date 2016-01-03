using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// Methods for converting values between Kelvin, Celsius and Fahrenheit
    /// </summary>
    public static class TemperatureUnitConvert
    {
        public static double KelvinToCelsius(double Kelvin)
        {
            if (Kelvin < 0)
                throw new Exception("Kelvin < 0!");

            return Kelvin - 273.15;
        }

        public static double CelsiusToKelvin(double Celsius)
        {
            return Celsius + 273.15;
        }

        public static double KelvinToFahrenheit(double Kelvin)
        {
            if (Kelvin < 0)
                throw new Exception("Kelvin < 0!");

            return Kelvin * 9.0 / 5.0 - 459.67;
        }

        public static double FahrenheitToKelvin(double Fahrenheit)
        {
            return (Fahrenheit + 459.67) * (5.0 / 9.0);
        }

        public static double FahrenheitToCelsius(double Fahrenheit)
        {
            return (Fahrenheit - 32.0) * (5.0 / 9.0);
        }

        public static double CelsiusToFahrenheit(double Celsius)
        {
            return Celsius * (9.0 / 5.0) + 32.0;
        }
    }
}
