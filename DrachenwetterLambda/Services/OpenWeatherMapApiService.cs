using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using KiteWeather.Enums;
using KiteWeather.Models;
using KiteWeather.OpenWeatherMapModels;
using Newtonsoft.Json;

namespace KiteWeather.Services
{
    public static class OpenWeatherMapApiService
    {
        public static PredictionsModel GetPredictions(this RequestModel request)
        {
            var model = request as PredictionsModel;
            model.Predictions = new List<Prediction>();
            AddCurrent(model);
            AddForecast(model);
            SelectDay(model);
            return model;
        }

        private static void AddCurrent(PredictionsModel model)
        {
            var current = GetCurrent(model.City).Result;
            model.Predictions.Add(current);
            model.Sunrise = current.Sys.SunriseTime;
            model.Sunset = current.Sys.SunsetTime;
        }

        private static void AddForecast(PredictionsModel model)
        {
            var forecast = GetForecast(model.City).Result;
            model.Predictions.AddRange(forecast.List);
        }

        private static void SelectDay(PredictionsModel model)
        {
            DateTime requestDate;
            switch (model.Day)
            {
                case Day.Today:
                    requestDate = DateTime.Now.Date;
                    break;
                case Day.Tomorrow:
                    requestDate = DateTime.Now.Date.AddDays(1);
                    break;
                default:
                    throw new ArgumentException("requested date not supported");
            }
            model.Predictions = model.Predictions.Where(p => p.Time.Date == requestDate).ToList();
        }

        private static async Task<Forecast> GetForecast(string city)
        {
            using (var wc = new HttpClient())
            {
                var url = $"{Settings.BaseUrl}forecast?q={city}&units={Settings.Units}&appid={Settings.AppId}";
                LambdaLogger.Log("GET " + url);
                var json = await wc.GetStringAsync(url);
                //LambdaLogger.Log("Result: " + json);
                return JsonConvert.DeserializeObject<Forecast>(json);
            }
        }

        private static async Task<Current> GetCurrent(string city)
        {
            using (var wc = new HttpClient())
            {
                var url = $"{Settings.BaseUrl}weather?q={city}&units={Settings.Units}&appid={Settings.AppId}";
                LambdaLogger.Log("GET " + url);
                var json = await wc.GetStringAsync(url);
                //LambdaLogger.Log("Result: " + json);
                return JsonConvert.DeserializeObject<Current>(json);
            }
        }
    }
}