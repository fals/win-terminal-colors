using System;
using System.Text.Json;

namespace dotnet_win_terminal_colors
{
    public class WindowsTerminalColorScheme
    {
        public WindowsTerminalColorScheme(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Background { get; set; }
        public string Black { get; set; }
        public string Blue { get; set; }
        public string BrightBlack { get; set; }
        public string BrightBlue { get; set; }
        public string BrightCyan { get; set; }
        public string BrightGreen { get; set; }
        public string BrightPurple { get; set; }
        public string BrightRed { get; set; }
        public string BrightWhite { get; set; }
        public string BrightYellow { get; set; }
        public string Cyan { get; set; }
        public string Foreground { get; set; }
        public string Green { get; set; }
        public string Name { get; set; }
        public string Purple { get; set; }
        public string Red { get; set; }
        public string White { get; set; }
        public string Yellow { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Serialize<WindowsTerminalColorScheme>(this, options);
        }
    }
}
