using System.Runtime.Serialization;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// Used for serialization of OpenWeatherMap JSON response. Contains original names of elements.
    /// </summary>
    /// 
    [DataContract]
    public class OpenWeatherJSONRoot
    {
        [DataMember]
        public City city { get; set; }
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public float message { get; set; }
        [DataMember]
        public int cnt { get; set; }
        [DataMember]
        public List[] list { get; set; }

        public TemperatureUnit temperatureUnit;
    }

    [DataContract]
    public class City
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int population { get; set; }
    }

    [DataContract]
    public class Coord
    {
        [DataMember]
        public float lon { get; set; }
        [DataMember]
        public float lat { get; set; }
    }

    [DataContract]
    public class List
    {
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Temp temp { get; set; }
        [DataMember]
        public float pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public Weather[] weather { get; set; }
        [DataMember]
        public float speed { get; set; }
        [DataMember]
        public int deg { get; set; }
        [DataMember]
        public int clouds { get; set; }
        [DataMember]
        public float rain { get; set; }
        [DataMember]
        public float snow { get; set; }
    }

    [DataContract]
    public class Temp
    {
        [DataMember]
        public float day { get; set; }
        [DataMember]
        public float min { get; set; }
        [DataMember]
        public float max { get; set; }
        [DataMember]
        public float night { get; set; }
        [DataMember]
        public float eve { get; set; }
        [DataMember]
        public float morn { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }
}
