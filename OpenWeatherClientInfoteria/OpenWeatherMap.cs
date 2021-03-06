﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    public class OpenWeatherMap : WeatherProvider
    {

        private string APIkey;

        private OpenWeatherJSONRoot result;
                                                
        public OpenWeatherMap(string apiKey = "1e277518425a18a62c387b27f1935738")
        {
            this.APIkey = apiKey;
            this.result = null;
            this.city = null;
            this.country = null;
            this.useLatLng = false;
        }


        public override async Task UpdateData()
        {
            using (var client = new HttpClient(new HttpClientHandler(), true))
            {
                string u;
                if (!useLatLng && this.city != null && this.city.Length > 0)
                     u = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&appid={1}&cnt={2}", this.city, this.APIkey, 16);
                else
                    u = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&appid={2}&cnt={3}", this.lat.ToString().Replace('.', ','), this.lng.ToString().Replace('.', ','), this.APIkey, 16);

                var res = await client.GetStringAsync(u);
                                                      
                MemoryStream stream1 = new MemoryStream(Encoding.UTF8.GetBytes(res));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(OpenWeatherJSONRoot));

                this.result = (OpenWeatherJSONRoot)ser.ReadObject(stream1);
                this.result.temperatureUnit = TemperatureUnit.Kelvin;

                this.city = this.result.city.name;
            }
        }
                                       
        public override async Task<DayWeatherInfo> GetWeather(DateTime day)
        {

            if (result.list.Count() == 0)
                await UpdateData();
                                                                 
            var x = result.list.First(i => UnixTimeStampToDateTime(i.dt).Date == day.Date);
                                                                 
            return GetDayWeatherInfo(x);
        }

        public DayWeatherInfo GetDayWeatherInfo(List l)
        {

            DayWeatherInfo res = new DayWeatherInfo();

            res.city = result.city.name;
            res.date = UnixTimeStampToDateTime(l.dt);
            res.humidity = l.humidity;
            res.pressure = l.pressure;
            res.rain = l.rain;
            res.tempDay = l.temp.day;
            res.tempEvening = l.temp.eve;
            res.tempMax = l.temp.max;
            res.tempMin = l.temp.min;
            res.tempMorning = l.temp.morn;
            res.tempNight = l.temp.night;
            res.weatherShortInfo = l.weather.First().description;
            res.windSpeed = l.speed;
            res.icon = l.weather.First().icon;

            return res;
        }

        public override async Task<List<DayWeatherInfo>> GetWeather()
        {

            if (result.list.Count() == 0)
                await UpdateData();

            List<DayWeatherInfo> res = new List<DayWeatherInfo>();

            foreach(List l in result.list)
            {
                res.Add(GetDayWeatherInfo(l));
            }

            return res;
        }


        public override DayWeatherInfo GetWeather(int index)
        {
            List x = result.list[index];

            return GetDayWeatherInfo(x);
        }
    }
}
