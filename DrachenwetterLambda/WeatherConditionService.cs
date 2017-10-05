﻿using System;
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
                return $"{predictionTime} Der Wind ist mit {Math.Round(current.Wind.SpeedKmH)} Stundenkilometern heute leider zu schwach um einen Drachen steigen zu lassen.";
            }
            if (current.Wind.SpeedKmH > 40)
            {
                return
                    $"{predictionTime} Es ist heute sehr stürmisch! Bei {Math.Round(current.Wind.SpeedKmH)} Stundenkilometern Wingeschwindigkeit solltest du mit deinem Drachen lieber zuhause bleiben.";
            }
            return
                $"{predictionTime} Mit einer Windgeschwindigkeit von {Math.Round(current.Wind.SpeedKmH)} Stundenkilometern ist gerade ideales Wetter um einen Drachen steigen zu lassen!";
        }

        public string GetTodayKiteWeather()
        {
            var forecast = GetForecast().Result;

            var goodConditions = forecast.List.Where(p => p.Wind.SpeedKmH < 40 && p.Wind.SpeedKmH > 15 && p.Time.Date == DateTime.Now.Date)
                .OrderBy(p => p.Time).ToList();

            if (goodConditions.Any())
            {
                return
                    $"Zwischen {goodConditions.First().Time.Hour} und {goodConditions.Last().Time.Hour} Uhr ist bei einer durchschnittlichen Windgeschwindigkeit von {Math.Round(goodConditions.AverageWindKmH())} Stundenkilometern heute ideales Wetter um Drachen steigen zu lassen";
            }
            var average = forecast.List.AverageWindKmH();
            if (average > 40)
            {
                return
                    $"Heute ist es mit durchschnittlich {Math.Round(average)} Stundenkilometern leider zu stürmisch für einen Drachen.";
            }
            return
                $"Der Wind ist heute mit etwa {Math.Round(average)} Stundenkilometern leider zu schwach um einen Drachen steigen zu lassen.";
        }


        private async Task<Forecast> GetForecast()
        {
            using (var wc = new HttpClient())
            {
                var url = $"{Settings.BaseUrl}forecast?q={Settings.City}&units={Settings.Units}&appid={Settings.AppId}";
                LambdaLogger.Log("GET " + url);
                var json = await wc.GetStringAsync(url);
                LambdaLogger.Log("Result: " + json);
                try
                {
                    return JsonConvert.DeserializeObject<Forecast>(json);
                }
                catch (Exception e)
                {
                    LambdaLogger.Log(e.Message);
                    throw;
                }
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