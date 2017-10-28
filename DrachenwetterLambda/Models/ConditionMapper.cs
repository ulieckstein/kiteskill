using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KiteWeather.Models
{
    public class ConditionMapper
    {
        public readonly string Description;

        public readonly string DescriptionTranslated;

        public readonly bool CanFly;

        public readonly int Weight;

        private ConditionMapper(string description, string descriptionTranslated, bool canFly, int weight)
        {
            Description = description;
            DescriptionTranslated = descriptionTranslated;
            CanFly = canFly;
            Weight = weight;
        }

        public static List<ConditionMapper> Conditions = new List<ConditionMapper>
        {
            new ConditionMapper("clear sky", "Es gibt außerdem Wolkenfreien Himmel.", true, 1),
            new ConditionMapper("few clouds", "Es gibt nur wenige Wolken.", true, 2),
            new ConditionMapper("scattered clouds", "Es gibt nur vereinzelte Wolken.", true, 3),
            new ConditionMapper("broken clouds", "Es gibt vereinzelte Wolken", true, 4),
            new ConditionMapper("shower rain", "Nimm aber besser eine Jacke mit, denn du musst du mit einzelnen Regenschauern rechnen.", true, 5),
            new ConditionMapper("rain", "Allerdings soll es regnen, also bleib besser zu hause.", false, 6),
            new ConditionMapper("thunderstorm", "Allerdings wirdes auch gewittern, also solltest du mit deinem Drachen auf jeden Fall im Haus bleiben.", false, 7),
            new ConditionMapper("snow", "Jedoch wird der Schnee deinen Drachen am fliegen hindern.", false, 8),
            new ConditionMapper("mist", "Ich fürchte jedoch, du wirst deinen Drachen im Nebel nicht sehen können", false, 9)
        };

        public static ConditionMapper GetDescription(string apiDescription)
        {
            return Conditions.Single(c => c.Description == apiDescription);
        }

        public static ConditionMapper GetByWeight(int weight)
        {
            return Conditions.Single(c => c.Weight == weight);
        }
    }

    public static class ConditionExtensions
    {
        public static ConditionMapper GetWorstCondition(this List<Prediction> conditions)
        {
            return ConditionMapper.GetByWeight(conditions.Max(p => p.Weather.Max(c => c.Mapped.Weight)));
        }
    }
    
}
