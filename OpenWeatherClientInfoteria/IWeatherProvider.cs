using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    public interface IWeatherProvider
    {
        bool SetCity(string City);
        string GetCity();
        bool SetCountry(string Country);
        Task UpdateData();
        Task<DayWeatherInfo> GetWeather(DateTime day);
        Task<DayWeatherInfo> GetWeather(int index);
        Task<List<DayWeatherInfo>> GetWeather();
    }
}
