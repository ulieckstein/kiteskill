using System.Collections.Generic;
using System.Linq;
using KiteWeather.OpenWeatherMapModels;

namespace KiteWeather.Models
{
    public class WindResultModel : PredictionsModel
    {
        public List<Prediction> GoodWindConditions { get; set; }
        public bool HasGoodWind => GoodWindConditions.Any();
    }
}