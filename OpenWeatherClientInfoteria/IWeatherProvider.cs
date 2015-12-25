using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// General interface, created for future upgrades (one interface - several providers)
    /// </summary>
    public interface IWeatherProvider
    {
        bool SetCity(string City);
        string GetCity();
        bool SetCountry(string Country);
        Task UpdateData();
        Task<DayWeatherInfo> GetWeather(DateTime day);
        DayWeatherInfo GetWeather(int index);
        Task<List<DayWeatherInfo>> GetWeather();
    }
}
