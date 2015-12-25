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
        void SetLatLng(double Lat, double Lng);
        bool UseLatLng();
        bool UseCityName();
        bool SetCity(string City);
        string GetCity();
        bool SetCountry(string Country);
        Task UpdateData();
        Task<DayWeatherInfo> GetWeather(DateTime day);
        DayWeatherInfo GetWeather(int index);
        Task<List<DayWeatherInfo>> GetWeather();
    }

    public abstract class WeatherProvider : IWeatherProvider
    {
        protected string city;
        protected string country;
        protected double lat;
        protected double lng;
        protected bool useLatLng = false;
        public bool SetCity(string City)
        {
            this.city = City;
            return true;
        }

        public bool SetCountry(string Country)
        {
            this.country = Country;
            return true;
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
