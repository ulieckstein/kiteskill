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

        public SkillResponse FunctionHandler(SkillRequest input)
        {
            
                var outputString = "hallo, wie kann ich dir helfen?";

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
                        case "GetTodaysKiteWeather":
                            outputString = GetTodaysKiteConditions();
                            break;
                        case "GetTomorrowsKiteWeatherIntent":
                            outputString = GetTomorrowsKiteConditions();
                        break;
                        default:
                            outputString = GetTodaysKiteConditions();
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

        private string GetTodaysKiteConditions()
        {
            return "du möchtest wissen, ob du heute einen drachen steigen lassen kannst";
        }

        private string GetTomorrowsKiteConditions()
        {
            return "du möchtest wissen, ob du morgen einen drachen steigen lassen kannst";
        }

    }
}
