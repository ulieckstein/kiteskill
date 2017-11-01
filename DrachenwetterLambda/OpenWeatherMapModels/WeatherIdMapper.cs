using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;

namespace KiteWeather.OpenWeatherMapModels
{
    public class WeatherIdMapper
    {
        public readonly int[] Ids;

        public readonly string OutputText;

        public readonly bool CanFly;

        public readonly int Weight;

        private WeatherIdMapper(int[] ids, string outputText, bool canFly, int weight)
        {
            Ids = ids;
            OutputText = outputText;
            CanFly = canFly;
            Weight = weight;
        }

        public static List<WeatherIdMapper> Conditions = new List<WeatherIdMapper>
        {
            new WeatherIdMapper(new int[]{951, 952,953,954,955,956,957,958,959,960,961,962}, "", true, 0),
            new WeatherIdMapper(new int[]{800}, "Es gibt außerdem Wolkenfreien Himmel.", true, 1),
            new WeatherIdMapper(new int[]{801, 802}, "Es gibt nur wenige Wolken.", true, 2),
            new WeatherIdMapper(new int[]{803, 804}, "Es wird jedoch recht bewölkt.", true, 3),
            new WeatherIdMapper(new int[]{500,501,520}, "Nimm aber besser eine Jacke mit, denn du musst du mit einzelnen Regenschauern rechnen.", true, 4),
            new WeatherIdMapper(new int[]{600,615,620,300,301,302,310,311,312,313,314,321}, "Allerdings soll es leichte Schneeschauer geben, also pack dich warm ein.", true, 5),
            new WeatherIdMapper(new int[]{502,503,504,511,521,522,531}, "Allerdings soll es regnen, also bleib besser zu hause.", false, 6),
            new WeatherIdMapper(new int[]{200,201,202,210,211,212,221,230,231,232}, "Allerdings wirdes auch gewittern, also solltest du mit deinem Drachen auf jeden Fall im Haus bleiben.", false, 7),
            new WeatherIdMapper(new int[]{601,602,611,612,616,621,622}, "Jedoch wird der Schnee deinen Drachen am fliegen hindern.", false, 8),
            new WeatherIdMapper(new int[]{701,711,721,731,741,751,761,762}, "Ich fürchte jedoch, du wirst deinen Drachen im Nebel nicht sehen können", false, 9),
            new WeatherIdMapper(new int[]{771,781}, "Allerdings ist ein Sturm angekündigt, also bleib besser Zuhause", false, 10),
            new WeatherIdMapper(new int[]{900,901,902,903,904,905,906}, "Es gibt jedoch eine Warnung zu extremen Wetterbedingungen, daher bleib lieber Zuhause", false, 11)
        };

        public static WeatherIdMapper GetById(int id)
        {
            LambdaLogger.Log("GetById " + id);
            return Conditions.SingleOrDefault(c => c.Ids.Contains(id));
        }

        public static WeatherIdMapper GetByWeight(int weight)
        {
            LambdaLogger.Log("GetConditionByWeight " + weight);
            return Conditions.SingleOrDefault(c => c.Weight == weight);
        }
    }

    public static class ConditionExtensions
    {
        public static WeatherIdMapper GetWorstCondition(this List<Prediction> conditions)
        {
            return WeatherIdMapper.GetByWeight(conditions.Max(p => p.Weather.Max(c => c.Mapped.Weight)));
        }
    }
    
}
