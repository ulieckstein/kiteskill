using System.Collections.Generic;

namespace KiteWeather.Models
{
    public class Forecast
    {
        public City City { get; set; }
        public IEnumerable<Prediction> List { get; set; }
    }
}
