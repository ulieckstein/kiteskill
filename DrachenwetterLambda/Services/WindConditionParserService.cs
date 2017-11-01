using System;
using System.Linq;
using Amazon.Lambda.Core;
using KiteWeather.Models;

namespace KiteWeather.Services
{
    public static class WindConditionParserService
    {
        private const int MinSpeed = 15;
        private const int MaxSpeed = 40;

        public static WindResultModel ParseWindConditions(this PredictionsModel predictions)
        {
            var model = predictions.ToWindResultModel();
            GetGoodWindPredictions(model);
            return model;
        }

        private static void GetGoodWindPredictions(WindResultModel model)
        {
            LambdaLogger.Log("Parsing Wind Conditions");
            model.GoodWindConditions = model.Predictions
                .Where(p => p.Wind.SpeedKmH < MaxSpeed && p.Wind.SpeedKmH > MinSpeed)
                .ToList();
        }
    }
}