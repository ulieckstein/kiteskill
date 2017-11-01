using System.Linq;
using Amazon.Lambda.Core;
using KiteWeather.Models;

namespace KiteWeather.Services
{
    public static class WeatherConditionsParserService
    {
        public static WindAndWeatherResultModel ParseWeatherConditions(this WindResultModel windResults)
        {
            var model = windResults.ToWindAndWeatherResultModel();
            if (!windResults.HasGoodWind) return model;
            GetGoodWeatherPredictions(model);
            return model;
        }

        private static void GetGoodWeatherPredictions(WindAndWeatherResultModel model)
        {
            LambdaLogger.Log("Parsing Weather Conditions");
            model.GoodWeatherConditions = model.GoodWindConditions
                .Where(p => p.Weather.Any(w => w.Mapped.CanFly))
                .ToList();
        }
    }
}
