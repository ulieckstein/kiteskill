using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace KiteWeather
{
    public class Function
    {
        private WeatherConditionService _weatherService;

        public SkillResponse FunctionHandler(SkillRequest input)
        {
                _weatherService = new WeatherConditionService();
                var outputString = string.Empty;

                if (input.GetRequestType() == typeof(LaunchRequest))
                    outputString = GetTodaysKiteConditions();
                else if (input.GetRequestType() == typeof(IntentRequest))
                    switch (((IntentRequest)input.Request).Intent.Name)
                    {
                        case "AMAZON.CancelIntent":
                        case "AMAZON.StopIntent":
                            outputString = "Bis bald";
                            break;
                        case "AMAZON.HelpIntent":
                            outputString =
                                "Ich kann dir helfen herauszufinden ob sich das Wetter in Koblenz heute oder morgen zum Drachen steigen lassen eignet";
                            break;
                        case "GetCurrentKiteWeatherIntent":
                            outputString = GetCurrentKiteConditions();
                            break;
                        case "GetTodaysKiteWeatherIntent":
                            outputString = GetTodaysKiteConditions();
                            break;
                        case "GetTomorrowsKiteWeatherIntent":
                            outputString = GetTomorrowsKiteConditions();
                        break;
                        default:
                            outputString = GetCurrentKiteConditions();
                            break;
                    }

            var response = new SkillResponse
            {
                Response = new ResponseBody
                {
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text = outputString
                    },
                    ShouldEndSession = true
                },
                Version = "1.0"
            };
            return response;
        }

        private string GetCurrentKiteConditions()
        {
            return _weatherService.GetCurrentKiteWeather();
        }

        private string GetTodaysKiteConditions()
        {
            return _weatherService.GetTodayKiteWeather();
        }

        private string GetTomorrowsKiteConditions()
        {
            return "du möchtest wissen, ob du morgen einen drachen steigen lassen kannst";
        }

    }
}
