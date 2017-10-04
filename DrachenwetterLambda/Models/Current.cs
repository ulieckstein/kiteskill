using System;

namespace KiteWeather.Models
{
    public class Current
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public DateTime Dt { get; set; }
        public MainPrediction Main { get; set; }
        public WindPrediction Wind { get; set; }
        public CloudsPrediction Clouds { get; set; }
        public WeatherPrediction Weather { get; set; }
    }
}
