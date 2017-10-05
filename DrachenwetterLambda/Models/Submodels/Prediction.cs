using System;
using System.Collections.Generic;

namespace KiteWeather.Models
{
    public struct Prediction
    {
        public long Dt { get; set; }

        public DateTime Time => Dt.ToLocalTime();
        public MainPrediction Main { get; set; }
        public WindPrediction Wind { get; set; }
        public CloudsPrediction Clouds { get; set; }
        public IEnumerable<WeatherPrediction> Weather { get; set; }
    }
}