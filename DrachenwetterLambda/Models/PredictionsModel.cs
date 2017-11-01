using System;
using System.Collections.Generic;
using KiteWeather.OpenWeatherMapModels;

namespace KiteWeather.Models
{
    public class PredictionsModel : RequestModel
    {
        public List<Prediction> Predictions { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }
}