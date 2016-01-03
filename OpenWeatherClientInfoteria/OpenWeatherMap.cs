using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace OpenWeatherClientInfoteria
{
    public class OpenWeatherMap : WeatherProvider
    {

        private string APIkey;

        private OpenWeatherJSONRoot result;
        
        private static readonly Uri service = new Uri("http://api.openweathermap.org/data/2.5");

        public OpenWeatherMap(TemperatureUnit unit = TemperatureUnit.Celsius, string apiKey = "1e277518425a18a62c387b27f1935738")
        {
            this.APIkey = apiKey;
            this.result = null;
            this.city = null;
            this.country = null;
            this.useLatLng = false;
            this.temperatureUnit = unit;
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

                if(this.temperatureUnit == TemperatureUnit.Celsius)
                {
                    u += "&units=metric";
                }
                else if(this.temperatureUnit == TemperatureUnit.Fahrenheit)
                {
                    u += "&units=imperial";
                }

                var res = await client.GetStringAsync(u);
                
                int cod = Int32.Parse((JsonObject.Parse(res))["cod"].GetString());

                if(cod == 404)
                {
                    throw new Exception("City not found!");
                }
                
                using (MemoryStream stream1 = new MemoryStream(Encoding.UTF8.GetBytes(res)))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(OpenWeatherJSONRoot));
                    
                    this.result = (OpenWeatherJSONRoot)ser.ReadObject(stream1);
                    this.result.temperatureUnit = TemperatureUnit.Kelvin;

                    this.city = this.result.city.name;
                }
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
            res.icon = "http://openweathermap.org/img/w/" + l.weather.First().icon + ".png";
            
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

        public override void SetTemperatureUnit(TemperatureUnit target)
        {

            foreach (List l in result.list)
            {
                l.temp.day = (float)ChangeTemperatureUnit(l.temp.day, temperatureUnit, target);
                l.temp.eve = (float)ChangeTemperatureUnit(l.temp.eve, temperatureUnit, target);
                l.temp.max = (float)ChangeTemperatureUnit(l.temp.max, temperatureUnit, target);
                l.temp.min = (float)ChangeTemperatureUnit(l.temp.min, temperatureUnit, target);
                l.temp.morn = (float)ChangeTemperatureUnit(l.temp.morn, temperatureUnit, target);
                l.temp.night = (float)ChangeTemperatureUnit(l.temp.night, temperatureUnit, target);
                
            }
            this.temperatureUnit = target;
        }
    }
}
