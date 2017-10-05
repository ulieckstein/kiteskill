using System;
using System.Diagnostics;
using KiteWeather;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var wcs = new WeatherConditionService();
            Console.WriteLine("Testing output for GetCurrentKiteWeather method:");
            Console.WriteLine(wcs.GetCurrentKiteWeather());

            Console.WriteLine();
            Console.WriteLine("press any key to exit");
            Console.Read();
        }
    }
}
