using System;
using System.Collections.Generic;
using System.Linq;
using KiteWeather.Enums;
using KiteWeather.OpenWeatherMapModels;

namespace KiteWeather.Models
{
    public class WindAndWeatherResultModel : WindResultModel
    {
        public List<Prediction> GoodWeatherConditions { get; set; }
        public bool HasGoodWeather => GoodWeatherConditions.Any();

        public override string ToString()
        {
            if (!HasGoodWind)
            {
                return BadWind();
            }
            if (!HasGoodWeather)
            {
                return BadWeather();
            }
            return GoodWindAndWeather();
        }

        private string BadWind()
        {
            var average = Predictions.AverageWindKmH();
            if (average > 40)
            {
                return
                    $"{OutputDay()} ist es mit durchschnittlich {Math.Round(average)} Stundenkilometern leider zu stürmisch für einen Drachen.";
            }
            return
                $"Der Wind ist {OutputDay()} mit etwa {Math.Round(average)} Stundenkilometern leider zu schwach um einen Drachen steigen zu lassen.";
        }

        private string BadWeather()
        {
            return $"Wir haben zwar mit {GoodWindConditions.AverageWindKmH()} Stundenkilometern heute guten Wind, " +
                   GoodWindConditions.GetWorstCondition().OutputText;
        }

        private string GoodWindAndWeather()
        {
            return
                $"Zwischen {GoodWeatherConditions.Min(p => p.Time).Hour} und {GoodWeatherConditions.Max(p => p.Time.AddHours(Settings.PredictionFrequencyHours)).Hour} Uhr " +
                $"ist bei einer durchschnittlichen Windgeschwindigkeit von {Math.Round(GoodWeatherConditions.AverageWindKmH())} Stundenkilometern " +
                "heute ideales Wetter um Drachen steigen zu lassen. " +
                GoodWeatherConditions.ToList().GetWorstCondition().OutputText;
        }

        private string OutputDay()
        {
            switch (Day)
            {
                case Day.Today:
                    return "Heute ";
                case Day.Tomorrow:
                    return "Morgen ";
                default:
                    throw new ArgumentException("Requested Day not supported");
            }
        }
    }
}