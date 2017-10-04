namespace KiteWeather.Models
{
    public struct WindPrediction
    {
        public double Speed { get; set; }

        public double SpeedKmH => Speed * 3.6;
    }
}