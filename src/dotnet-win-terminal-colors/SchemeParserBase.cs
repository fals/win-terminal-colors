using System.IO;

namespace dotnet_win_terminal_colors
{
    internal abstract class SchemeParserBase : ISchemeParser
    {
        public abstract string Name { get; }

        public abstract bool CanParse(string schemeName);

        public abstract WindowsTerminalColorScheme ParseScheme(string schemeName);

        // Common elements and helpers
        protected abstract string FileExtension { get; }

        protected string ExtractSchemeName(string schemeFileName)
        {
            var name = Path.GetFileName(schemeFileName);

            return name.Substring(0, name.Length - FileExtension.Length);
        }

        protected void FillColor(WindowsTerminalColorScheme color, ColorPallete colorPallete, string hexadecimal)
        {
            switch (colorPallete)
            {
                case ColorPallete.DarkBlack:
                    color.Black = hexadecimal;
                    break;
                case ColorPallete.DarkBlue:
                    color.Blue = hexadecimal;
                    break;
                case ColorPallete.DarkGreen:
                    color.Green = hexadecimal;
                    break;
                case ColorPallete.DarkCyan:
                    color.Cyan = hexadecimal;
                    break;
                case ColorPallete.DarkRed:
                    color.Red = hexadecimal;
                    break;
                case ColorPallete.DarkMagenta:
                    color.Purple = hexadecimal;
                    break;
                case ColorPallete.DarkYellow:
                    color.Yellow = hexadecimal;
                    break;
                case ColorPallete.DarkWhite:
                    color.White = hexadecimal;
                    break;
                case ColorPallete.BrightBlack:
                    color.BrightBlack = hexadecimal;
                    break;
                case ColorPallete.BrightBlue:
                    color.BrightBlue = hexadecimal;
                    break;
                case ColorPallete.BrightGreen:
                    color.BrightGreen = hexadecimal;
                    break;
                case ColorPallete.BrightCyan:
                    color.BrightCyan = hexadecimal;
                    break;
                case ColorPallete.BrightRed:
                    color.BrightRed = hexadecimal;
                    break;
                case ColorPallete.BrightMagenta:
                    color.BrightPurple = hexadecimal;
                    break;
                case ColorPallete.BrightYellow:
                    color.BrightYellow = hexadecimal;
                    break;
                case ColorPallete.BrightWhite:
                    color.BrightWhite = hexadecimal;
                    break;
                default:
                    break;
            }
        }
    }
}
