using System;

namespace KiteWeather.OpenWeatherMapModels
{
    public class SunPrediction
    {
        public long Sunrise { get; set; }
        public DateTime SunriseTime => Sunrise.ToLocalTime();
        public long Sunset { get; set; }
        public DateTime SunsetTime => Sunset.ToLocalTime();
    }
}
