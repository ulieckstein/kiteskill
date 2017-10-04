using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleTest
{
    public class MenuParser
    {
        private StringBuilder _outputString;

        public string GetTodaysMenu()
        {
            var todayString = DateTime.Today.ToString("yyyy-MM-dd");
            var documentUrl = "http://www.studierendenwerk-koblenz.de/api/speiseplan/speiseplan.xml";
            _outputString = new StringBuilder();

            using (var reader = XmlReader.Create(documentUrl))
            {
                reader.ReadStartElement("Mensamenue");

                while (!reader.EOF)
                {
                    if (reader.NodeType == XmlNodeType.Element
                        && reader.Name.Equals("Datum"))
                    {
                        // this advances the reader...so it's either XNode.ReadFrom() or reader.Read(), but not both
                        var matchedElement = XNode.ReadFrom(reader) as XElement;
                        if (matchedElement != null && matchedElement.FirstNode.ToString() == todayString)
                        {
                            ParseDay(matchedElement);
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }
            }

            return _outputString.ToString();
        }

        private void ParseDay(XNode dayNode)
        {
            using (var nodeReader = dayNode.CreateReader())
            {
                while (!nodeReader.EOF)
                {
                    nodeReader.Read();
                    ParseMenu(nodeReader);
                }
            }
        }

        private void ParseMenu(XmlReader reader)
        {
            foreach (var menu in _allMenues)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == menu.Identifyer)
                {
                    _outputString.Append(menu.OutputText);
                    reader.Read();
                    if (reader.NodeType == XmlNodeType.CDATA && reader.HasValue)
                    {
                        _outputString.Append(RemoveBracketText(reader.Value));
                        _outputString.Append(".");
                    }
                    else
                    {
                        _outputString.Append("Keine Angabe.");
                    }
                }
            }
        }

        private string RemoveBracketText(string input)
        {
            return Regex.Replace(input, @" ?\(.*?\)", string.Empty);
        }

        private readonly Menu[] _allMenues =
        {
            new Menu
            {
                Identifyer = "menu1",
                OutputText = "Menu eins "
            },
            new Menu
            {
                Identifyer = "menuv",
                OutputText = "Als Vegetarisches Gericht "
            },
            new Menu
            {
                Identifyer = "menue",
                OutputText = "An der Extratheke "
            },
            new Menu
            {
                Identifyer = "menud",
                OutputText = "Als Vitalteller "
            },
            new Menu
            {
                Identifyer = "menuvegan",
                OutputText = "Und für die Veganer "
            }
        };

        internal class Menu
        {
            public string Identifyer { get; set; }
            public string OutputText { get; set; }
        }
    }
}