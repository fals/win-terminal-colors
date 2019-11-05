using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace dotnet_win_terminal_colors
{
    class XmlSchemeParser : SchemeParserBase
    {
        // In Windows Color Table order
        private static readonly Dictionary<string, ColorPallete> PListColorNames = new Dictionary<string, ColorPallete>()
        {
            ["Ansi 0 Color"] = ColorPallete.DarkBlack,
            ["Ansi 4 Color"] = ColorPallete.DarkBlue,
            ["Ansi 2 Color"] = ColorPallete.DarkGreen,
            ["Ansi 6 Color"] = ColorPallete.DarkCyan,
            ["Ansi 1 Color"] = ColorPallete.DarkRed,
            ["Ansi 5 Color"] = ColorPallete.DarkMagenta,
            ["Ansi 3 Color"] = ColorPallete.DarkYellow,
            ["Ansi 7 Color"] = ColorPallete.DarkWhite,
            ["Ansi 8 Color"] = ColorPallete.BrightBlack,
            ["Ansi 12 Color"] = ColorPallete.BrightBlue,
            ["Ansi 10 Color"] = ColorPallete.BrightGreen,
            ["Ansi 14 Color"] = ColorPallete.BrightCyan,
            ["Ansi 9 Color"] = ColorPallete.BrightRed,
            ["Ansi 13 Color"] = ColorPallete.BrightMagenta,
            ["Ansi 11 Color"] = ColorPallete.BrightYellow,
            ["Ansi 15 Color"] = ColorPallete.BrightWhite
        };

        private const string ForegroundKey = "Foreground Color";
        private const string BackgroundKey = "Background Color";
        private const string RedKey = "Red Component";
        private const string GreenKey = "Green Component";
        private const string BlueKey = "Blue Component";

        protected override string FileExtension { get; } = ".itermcolors";

        public override string Name { get; } = "iTerm Parser";

        public override bool CanParse(string schemeName) =>
            string.Equals(Path.GetExtension(schemeName), FileExtension, StringComparison.OrdinalIgnoreCase);

        public override WindowsTerminalColorScheme ParseScheme(string schemeName)
        {
            var colorName = ExtractSchemeName(schemeName);
            var color = new WindowsTerminalColorScheme(colorName);
            XmlDocument xmlDoc = LoadXmlScheme(schemeName); // Create an XML document object
            if (xmlDoc == null) return null;
            XmlNode root = xmlDoc.GetElementsByTagName("dict")[0];
            XmlNodeList children = root.ChildNodes;

            foreach (var tableEntry in children.OfType<XmlNode>().Where(_ => _.Name == "key"))
            {
                XmlNode components = tableEntry.NextSibling;
                string hexadecimal = ParseRgbFromXml(components);

                if (PListColorNames.TryGetValue(tableEntry.InnerText, out var colorPallete))
                {
                    FillColor(color, colorPallete, hexadecimal);
                }

                if (tableEntry.InnerText == ForegroundKey) { color.Foreground = hexadecimal; }
                else if (tableEntry.InnerText == BackgroundKey) { color.Background = hexadecimal; }
            }

            return color;
        }

        private static string ParseRgbFromXml(XmlNode components)
        {
            int r = -1;
            int g = -1;
            int b = -1;

            foreach (XmlNode c in components.ChildNodes)
            {
                if (c.Name == "key")
                {
                    if (c.InnerText == RedKey)
                    {
                        r = (int)(255 * Convert.ToDouble(c.NextSibling.InnerText, CultureInfo.InvariantCulture));
                    }
                    else if (c.InnerText == GreenKey)
                    {
                        g = (int)(255 * Convert.ToDouble(c.NextSibling.InnerText, CultureInfo.InvariantCulture));
                    }
                    else if (c.InnerText == BlueKey)
                    {
                        b = (int)(255 * Convert.ToDouble(c.NextSibling.InnerText, CultureInfo.InvariantCulture));
                    }
                }
            }

            if (r < 0 || g < 0 || b < 0)
            {
                Console.WriteLine("InvalidColor");

                return null;
            }

            return string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b);
        }

        private XmlDocument LoadXmlScheme(string schemeName)
        {
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object

            try
            {
                xmlDoc.Load(schemeName);
                return xmlDoc;
            }
            catch (XmlException /*e*/) { /* failed to parse */ }
            catch (IOException /*e*/) { /* failed to find */ }
            catch (UnauthorizedAccessException /*e*/) { /* unauthorized */ }

            return null;
        }
    }
}
