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
    }

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

        public virtual Task UpdateData()
        {
            throw new NotImplementedException();
        }

        public virtual Task<DayWeatherInfo> GetWeather(DateTime day)
        {
            throw new NotImplementedException();
        }

        public virtual DayWeatherInfo GetWeather(int index)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<DayWeatherInfo>> GetWeather()
        {
            throw new NotImplementedException();
        }
    }
}
