using System.Collections.Generic;

namespace KiteWeather.OpenWeatherMapModels
{
    public class Forecast
    {
        public City City { get; set; }
        public IEnumerable<Prediction> List { get; set; }
    }
}
