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
    /// <summary>
    /// Provides general information of wather data
    /// </summary>
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
        /// <summary>
        /// Link to a icon
        /// </summary>
        public string icon { get; set; }
                                                                    
    }

    
}
