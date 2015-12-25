using System;
using System.ComponentModel;

namespace OpenWeatherClientInfoteria
{
    public enum TemperatureUnit
    {
        Kelvin = 1,
        Fahrenheit = 2,
        Celsius = 3
    }
    public class DayWeatherInfo
    {
        public string city { get; set; }
        public DateTime date { get; set; }
        public double tempMax { get; set; }
        public double tempMin { get; set; }
        public double tempDay { get; set; }
        public double tempNight { get; set; }
        public double tempEvening { get; set; }
        public double tempMorning { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double windSpeed { get; set; }
        public double rain { get; set; }
        public string weatherShortInfo { get; set; }
        public TemperatureUnit tempUnit { get; set; }
                                                                    
    }

    public static class Convert
    {
        public static double KelvinToCelsius(double Kelvin)
        {
            if (Kelvin < 0)
                throw new Exception("Kelvin < 0!");

            return Kelvin - 273.15;
        }

        public static double KelvinToFahrenheit(double Kelvin)
        {
            if (Kelvin < 0)
                throw new Exception("Kelvin < 0!");
            
            return Kelvin * 9 / 5 - 459.67;
        }
    }
}
