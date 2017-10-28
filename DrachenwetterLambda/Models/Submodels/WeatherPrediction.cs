namespace KiteWeather.Models
{
    public struct WeatherPrediction
    {
        public string Main { get; set; }
        public string Desctiption { get; set; }
        public string Icon { get; set; }

        public ConditionMapper Mapped => ConditionMapper.GetDescription(Desctiption);
    }
}