using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// Interface for weather providers
    /// </summary>
    public interface IWeatherProvider
    {
        void SetLatLng(double Lat, double Lng);
        bool UseLatLng();
        bool UseCityName();
        void SetCity(string City);
        string GetCity();
        void SetCountry(string Country);
        Task UpdateData();
        Task<DayWeatherInfo> GetWeather(DateTime day);
        DayWeatherInfo GetWeather(int index);
        Task<List<DayWeatherInfo>> GetWeather();
        void SetTemperatureUnit(TemperatureUnit target);
    }

    
}
