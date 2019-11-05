namespace dotnet_win_terminal_colors
{
    interface ISchemeParser
    {
        string Name { get; }
        bool CanParse(string schemeName);
        WindowsTerminalColorScheme ParseScheme(string schemeName);
    }
}
