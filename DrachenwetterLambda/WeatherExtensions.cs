using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KiteWeather.OpenWeatherMapModels;

namespace KiteWeather
{
    public static class WeatherExtensions
    {
        public static DateTime ToLocalTime(this long apiTime)
        {
            var serverTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(apiTime).ToLocalTime();
            return serverTime.AddHours(1); //IRL to GER
        }

        public static double AverageWindKmH(this IEnumerable<Prediction> predictions)
        {
            var list = predictions as IList<Prediction> ?? predictions.ToList();
            var sum = list.Sum(p => p.Wind.SpeedKmH);
            return sum / list.Count();
        }
    }
}