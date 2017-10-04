using System.Text.RegularExpressions;

namespace MensaPlanLambda
{
    public static class StringHelpers
    {
        public static string RemoveUnwantedTextParts(this string input)
        {
            return input.Replace("In der Haupttheke!!", string.Empty);
        }

        public static string RemoveUnsupportedCharacters(this string input)
        {
            return input.Replace("&", "und");
        }

        public static string RemoveBracketText(this string input)
        {
            return Regex.Replace(input, @" ?\(.*?\)", string.Empty);
        }

        public static string FixPeriods(this string input)
        {
            return input
                .Replace(" .", ".")
                .Replace(".", ". ")
                .Replace("!!.", ".")
                .Replace("!.", ".");
        }
    }
}