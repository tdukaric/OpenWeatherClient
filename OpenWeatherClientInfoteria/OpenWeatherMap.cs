using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    public class OpenWeatherMap : IWeatherProvider
    {
        private string city;
        private string country;
        private string APIkey;

        private Rootobject result;

        public OpenWeatherMap()
        {
            this.APIkey = "2de143494c0b295cca9337e1e96b00e0";
            this.result = null;
            this.city = null;
            this.country = null;
        }

        public OpenWeatherMap(string apiKey)
        {
            this.APIkey = apiKey;
            this.result = null;
            this.city = null;
            this.country = null;
        }

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

        public async Task UpdateData()
        {
            using (var client = new HttpClient(new HttpClientHandler(), true))
            {
                string u = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&appid={1}&cnt={2}", this.city, this.APIkey, 16);

                var res = await client.GetStringAsync(u);

                //string res = "{\"city\":{\"id\":2643743,\"name\":\"London\",\"coord\":{\"lon\":-0.12574,\"lat\":51.50853},\"country\":\"GB\",\"population\":0},\"cod\":\"200\",\"message\":0.0255,\"cnt\":16,\"list\":[{\"dt\":1450868400,\"temp\":{\"day\":10.18,\"min\":7.99,\"max\":10.54,\"night\":10.54,\"eve\":9.96,\"morn\":9.33},\"pressure\":1025.86,\"humidity\":80,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"02d\"}],\"speed\":8.71,\"deg\":235,\"clouds\":8},{\"dt\":1450954800,\"temp\":{\"day\":11.19,\"min\":5.64,\"max\":11.19,\"night\":5.86,\"eve\":5.94,\"morn\":10.59},\"pressure\":1012.14,\"humidity\":94,\"weather\":[{\"id\":501,\"main\":\"Rain\",\"description\":\"moderate rain\",\"icon\":\"10d\"}],\"speed\":12.96,\"deg\":214,\"clouds\":92,\"rain\":3.21},{\"dt\":1451044800,\"temp\":{\"day\":9.3,\"min\":6.84,\"max\":13.17,\"night\":13.17,\"eve\":12.35,\"morn\":6.84},\"pressure\":1026.62,\"humidity\":95,\"weather\":[{\"id\":501,\"main\":\"Rain\",\"description\":\"moderate rain\",\"icon\":\"10d\"}],\"speed\":5.01,\"deg\":186,\"clouds\":100,\"rain\":4.38},{\"dt\":1451131200,\"temp\":{\"day\":14.51,\"min\":12.56,\"max\":14.51,\"night\":12.56,\"eve\":13.61,\"morn\":13.81},\"pressure\":1022.66,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":13.21,\"deg\":229,\"clouds\":80,\"rain\":1.29},{\"dt\":1451217600,\"temp\":{\"day\":12.39,\"min\":10.87,\"max\":12.39,\"night\":10.87,\"eve\":11.69,\"morn\":11.62},\"pressure\":1032.86,\"humidity\":0,\"weather\":[{\"id\":501,\"main\":\"Rain\",\"description\":\"moderate rain\",\"icon\":\"10d\"}],\"speed\":2.82,\"deg\":216,\"clouds\":99,\"rain\":7.97},{\"dt\":1451304000,\"temp\":{\"day\":11.83,\"min\":7.23,\"max\":11.83,\"night\":7.23,\"eve\":11.12,\"morn\":10.32},\"pressure\":1036.73,\"humidity\":0,\"weather\":[{\"id\":501,\"main\":\"Rain\",\"description\":\"moderate rain\",\"icon\":\"10d\"}],\"speed\":2.86,\"deg\":96,\"clouds\":83,\"rain\":9.29},{\"dt\":1451390400,\"temp\":{\"day\":6.33,\"min\":4.4,\"max\":6.33,\"night\":4.4,\"eve\":4.81,\"morn\":5.83},\"pressure\":1040.8,\"humidity\":0,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":7.99,\"deg\":121,\"clouds\":13},{\"dt\":1451476800,\"temp\":{\"day\":5.66,\"min\":2.58,\"max\":5.66,\"night\":5.06,\"eve\":2.58,\"morn\":3.06},\"pressure\":1037.77,\"humidity\":0,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":3.58,\"deg\":133,\"clouds\":0},{\"dt\":1451563200,\"temp\":{\"day\":6.55,\"min\":4,\"max\":6.55,\"night\":4.03,\"eve\":4,\"morn\":5.68},\"pressure\":1030.8,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":5.64,\"deg\":132,\"clouds\":57},{\"dt\":1451649600,\"temp\":{\"day\":3.87,\"min\":-0.89,\"max\":3.87,\"night\":-0.89,\"eve\":1.67,\"morn\":2.84},\"pressure\":1027.6,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":5.98,\"deg\":131,\"clouds\":30},{\"dt\":1451736000,\"temp\":{\"day\":1.79,\"min\":-3.23,\"max\":1.79,\"night\":1.11,\"eve\":0.5,\"morn\":-3.23},\"pressure\":1030.98,\"humidity\":0,\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"sky is clear\",\"icon\":\"01d\"}],\"speed\":2.57,\"deg\":183,\"clouds\":0,\"snow\":0.01},{\"dt\":1451822400,\"temp\":{\"day\":2.7,\"min\":0.82,\"max\":2.7,\"night\":0.82,\"eve\":1.07,\"morn\":1.21},\"pressure\":1032.2,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":4.56,\"deg\":117,\"clouds\":75,\"rain\":2.6},{\"dt\":1451908800,\"temp\":{\"day\":7.73,\"min\":2.55,\"max\":10.41,\"night\":10.41,\"eve\":7.75,\"morn\":2.55},\"pressure\":1019.35,\"humidity\":0,\"weather\":[{\"id\":501,\"main\":\"Rain\",\"description\":\"moderate rain\",\"icon\":\"10d\"}],\"speed\":4.63,\"deg\":211,\"clouds\":98,\"rain\":5.53},{\"dt\":1451995200,\"temp\":{\"day\":10.28,\"min\":5.9,\"max\":10.45,\"night\":5.9,\"eve\":7.41,\"morn\":10.45},\"pressure\":1006.15,\"humidity\":0,\"weather\":[{\"id\":501,\"main\":\"Rain\",\"description\":\"moderate rain\",\"icon\":\"10d\"}],\"speed\":1.49,\"deg\":347,\"clouds\":46,\"rain\":7.38},{\"dt\":1452081600,\"temp\":{\"day\":6.29,\"min\":2.89,\"max\":10.23,\"night\":10.23,\"eve\":7.01,\"morn\":2.89},\"pressure\":1030.15,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":3.3,\"deg\":227,\"clouds\":0,\"rain\":0.98},{\"dt\":1452168000,\"temp\":{\"day\":10.23,\"min\":10.23,\"max\":10.23,\"night\":10.23,\"eve\":10.23,\"morn\":10.23},\"pressure\":1025.51,\"humidity\":0,\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10d\"}],\"speed\":7.48,\"deg\":211,\"clouds\":97,\"rain\":0.95}]}";

                MemoryStream stream1 = new MemoryStream(Encoding.UTF8.GetBytes(res));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Rootobject));

                this.result = (Rootobject)ser.ReadObject(stream1);
                this.result.temperatureUnit = TemperatureUnit.Kelvin;

                this.city = this.result.city.name;
            }
        }


        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public async Task<DayWeatherInfo> GetWeather(DateTime day)
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

            return res;
        }

        public async Task<List<DayWeatherInfo>> GetWeather()
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

        public string GetCity()
        {
            return city;
        }

        public DayWeatherInfo GetWeather(int index)
        {
            List x = result.list[index];

            return GetDayWeatherInfo(x);
        }
    }
}
