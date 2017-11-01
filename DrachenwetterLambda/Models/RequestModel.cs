using System;
using KiteWeather.Enums;

namespace KiteWeather.Models
{
    public class RequestModel
    {
        public string City { get; set; }
        public Day Day { get; set; }
    }
}