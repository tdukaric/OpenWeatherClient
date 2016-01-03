using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherClientInfoteria
{
    /// <summary>
    /// Used for data binding
    /// </summary>
    public class WeatherListViewItem
    {
        public string DateBox { get; set; }
        public string TempBox { get; set; }
        public string DescBox { get; set; }
        public string Icon { get; set; }
    }
}
