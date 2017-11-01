using System.Collections.Generic;
using System.Linq;
using KiteWeather.OpenWeatherMapModels;

namespace KiteWeather.Models
{
    public class WindAndWeatherResultModel : WindResultModel
    {
        public List<Prediction> GoodWeatherConditions { get; set; }
        public bool HasGoodWeather => GoodWeatherConditions.Any();
    }
}