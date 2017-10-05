using System;

namespace KiteWeather.Models
{
    public struct Prediction
    {
        public long Dt { get; set; }

        public DateTime Time => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Dt);
        public MainPrediction Main { get; set; }
        public WindPrediction Wind { get; set; }
        public CloudsPrediction Clouds { get; set; }
        public WeatherPrediction Weather { get; set; }
    }
}