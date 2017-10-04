using System;

namespace KiteWeather.Models
{
    public struct Prediction
    {
        public DateTime Dt { get; set; }
        public MainPrediction Main { get; set; }
        public WindPrediction Wind { get; set; }
        public CloudsPrediction Clouds { get; set; }
        public WeatherPrediction Weather { get; set; }
    }
}