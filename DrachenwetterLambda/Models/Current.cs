using System;
using System.Collections.Generic;

namespace KiteWeather.Models
{
    public class Current
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public long Dt { get; set; }
        public MainPrediction Main { get; set; }
        public WindPrediction Wind { get; set; }
        public CloudsPrediction Clouds { get; set; }
        public IEnumerable<WeatherPrediction> Weather { get; set; }
        public DateTime Time => Dt.ToLocalTime();
    }
}
