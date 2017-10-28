using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using KiteWeather.Models;
using Newtonsoft.Json;

namespace KiteWeather
{

    public class WeatherConditionService
    {
        public string GetCurrentKiteWeather()
        {
            var current = GetCurrent().Result;
            var predictionTime = $"Hier das aktuelle Wetter von {current.Time.Hour} Uhr {current.Time.Minute}: ";
            if (current.Wind.SpeedKmH < 15.0)
            {
                return $"{predictionTime} Der Wind ist mit {Math.Round(current.Wind.SpeedKmH)} Stundenkilometern gerade leider zu schwach um einen Drachen steigen zu lassen.";
            }
            if (current.Wind.SpeedKmH > 40)
            {
                return
                    $"{predictionTime} Es ist im Moment sehr stürmisch! Bei {Math.Round(current.Wind.SpeedKmH)} Stundenkilometern Windgeschwindigkeit solltest du mit deinem Drachen lieber zuhause bleiben.";
            }
            return
                $"{predictionTime} Mit einer Windgeschwindigkeit von {Math.Round(current.Wind.SpeedKmH)} Stundenkilometern ist gerade ideales Wetter um einen Drachen steigen zu lassen!";
        }

        public string GetTodayKiteWeather()
        {
            var predictions = new List<Prediction> {GetCurrent().Result};
            predictions.AddRange(GetForecast().Result.List);

            return CreateWindSpeedResult(predictions);
        }

        private string CreateWindSpeedResult(List<Prediction> predictions)
        {
            var goodConditions = predictions
                .Where(p => p.Wind.SpeedKmH < 40 && p.Wind.SpeedKmH > 15 && p.Time.Date == DateTime.Now.Date)
                .ToList();

            if (goodConditions.Any())
            {
                return CreateWeatherResult(goodConditions);
            }
            var average = predictions.AverageWindKmH();
            if (average > 40)
            {
                return
                    $"Heute ist es mit durchschnittlich {Math.Round(average)} Stundenkilometern leider zu stürmisch für einen Drachen.";
            }
            return
                $"Der Wind ist heute mit etwa {Math.Round(average)} Stundenkilometern leider zu schwach um einen Drachen steigen zu lassen.";
        }

        private string CreateWeatherResult(List<Prediction> goodWindPredictions)
        {
            var goodWeatherConditions = goodWindPredictions.Where(p => p.Weather.Any(w => w.Mapped.CanFly)).ToList();

            if (goodWeatherConditions.Any())
            {
                return
                    $"Zwischen {goodWindPredictions.Min(p => p.Time).Hour} und {goodWindPredictions.Max(p => p.Time).Hour} Uhr "+
                    $"ist bei einer durchschnittlichen Windgeschwindigkeit von {Math.Round(goodWindPredictions.AverageWindKmH())} Stundenkilometern " +
                    "heute ideales Wetter um Drachen steigen zu lassen" +
                    goodWeatherConditions.ToList().GetWorstCondition().DescriptionTranslated;
            }

            return $"Wir haben zwar mit {goodWindPredictions.AverageWindKmH()} Stundenkilometern heute guten Wind, " +
                goodWindPredictions.GetWorstCondition().DescriptionTranslated;
        }

        private async Task<Forecast> GetForecast()
        {
            using (var wc = new HttpClient())
            {
                var url = $"{Settings.BaseUrl}forecast?q={Settings.City}&units={Settings.Units}&appid={Settings.AppId}";
                LambdaLogger.Log("GET " + url);
                var json = await wc.GetStringAsync(url);
                LambdaLogger.Log("Result: " + json);
                return JsonConvert.DeserializeObject<Forecast>(json);
            }
        }

        private async Task<Current> GetCurrent()
        {
            using (var wc = new HttpClient())
            {
                var url = $"{Settings.BaseUrl}weather?q={Settings.City}&units={Settings.Units}&appid={Settings.AppId}";
                LambdaLogger.Log("GET " + url);
                var json = await wc.GetStringAsync(url);
                LambdaLogger.Log("Result: " + json);
                return JsonConvert.DeserializeObject<Current>(json);
            }
        }
    }
}