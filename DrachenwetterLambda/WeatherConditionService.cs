﻿using System.Net.Http;
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
            if (current.Wind.SpeedKmH < 15.0)
            {
                return $"Der Wind ist mit {current.Wind.SpeedKmH} Stundenkilometern heute leider zu schwach um einen Drachen steigen zu lassen.";
            }
            if (current.Wind.SpeedKmH > 40)
            {
                return
                    $"Es ist heute sehr stürmisch! Bei {current.Wind.SpeedKmH} Stundenkilometern Wingeschwindigkeit solltest du mit deinem Drachen lieber zuhause bleiben.";
            }
            return
                $"Mit einer Windgeschwindigkeit von {current.Wind.SpeedKmH} Stundenkilometern ist gerade ideales Wetter um einen Drachen steigen zu lassen!";
        }

        private async Task<Forecast> GetForecast()
        {
            using (var wc = new HttpClient())
            {
                var json = await wc.GetStringAsync($"{Settings.BaseUrl}forecast?q={Settings.City}&units={Settings.Units}&appid={Settings.AppId}");
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