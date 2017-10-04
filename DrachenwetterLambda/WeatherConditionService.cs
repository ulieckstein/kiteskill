using System.Threading.Tasks;
using KiteWeather.Models;
using Newtonsoft.Json;

namespace KiteWeather
{

    public class WeatherConditionService
    {
        public WeatherConditionService()
        {
            
        }

        public async Task<string> GetCurrentKiteWeather()
        {
            var current = await GetCurrent();
            var windActivity = string.Empty;
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
            using (var wc = new System.Net.Http.HttpClient())
            {
                var json = await wc.GetStringAsync($"{Settings.BaseUrl}forecast?q={Settings.City}&units={Settings.Units}&appid={Settings.AppId}");
                return JsonConvert.DeserializeObject<Forecast>(json);
            }
        }

        private async Task<Current> GetCurrent()
        {
            using (var wc = new System.Net.Http.HttpClient())
            {
                var json = await wc.GetStringAsync($"{Settings.BaseUrl}current?q={Settings.City}&units={Settings.Units}&appid={Settings.AppId}");
                return JsonConvert.DeserializeObject<Current>(json);
            }
        }
    }
}