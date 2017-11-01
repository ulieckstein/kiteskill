namespace KiteWeather.OpenWeatherMapModels
{
    public struct WeatherPrediction
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public WeatherIdMapper Mapped => WeatherIdMapper.GetById(Id);
    }
}