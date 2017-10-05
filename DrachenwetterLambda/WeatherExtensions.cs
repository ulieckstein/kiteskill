using System;

namespace KiteWeather
{
    public static class WeatherExtensions
    {
        public static DateTime ToLocalTime(this long apiTime)
        {
            var serverTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(apiTime).ToLocalTime();
            return serverTime.AddHours(1); //IRL to GER
        }
    }
}