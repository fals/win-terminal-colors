using System;
using System.Reflection;

namespace dotnet_win_terminal_colors
{
    class Program
    {
        static void Main(string[] args)
        {
            var wtc = @"
██╗    ██╗██╗███╗   ██╗██████╗  ██████╗ ██╗    ██╗███████╗                                                             
██║    ██║██║████╗  ██║██╔══██╗██╔═══██╗██║    ██║██╔════╝                                                             
██║ █╗ ██║██║██╔██╗ ██║██║  ██║██║   ██║██║ █╗ ██║███████╗                                                             
██║███╗██║██║██║╚██╗██║██║  ██║██║   ██║██║███╗██║╚════██║                                                             
╚███╔███╔╝██║██║ ╚████║██████╔╝╚██████╔╝╚███╔███╔╝███████║                                                             
 ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═════╝  ╚═════╝  ╚══╝╚══╝ ╚══════╝                                                             
████████╗███████╗██████╗ ███╗   ███╗██╗███╗   ██╗ █████╗ ██╗          ██████╗ ██████╗ ██╗      ██████╗ ██████╗ ███████╗
╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔══██╗██║         ██╔════╝██╔═══██╗██║     ██╔═══██╗██╔══██╗██╔════╝
   ██║   █████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║███████║██║         ██║     ██║   ██║██║     ██║   ██║██████╔╝███████╗
   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██╔══██║██║         ██║     ██║   ██║██║     ██║   ██║██╔══██╗╚════██║
   ██║   ███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██║  ██║███████╗    ╚██████╗╚██████╔╝███████╗╚██████╔╝██║  ██║███████║
   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝";

            Console.WriteLine(wtc);

            if (args.Length == 0)
            {
                var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

                Console.WriteLine($"wtc v{versionString}");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Currently support: iTerm/iTerm2 color schemes");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Usage:");
                Console.WriteLine("  wtc fullpath/schemename.itermcolors");
                Console.WriteLine("----------------------------------------------");
            }
            else
            {
                var parser = new XmlSchemeParser();
                var json = parser.ParseScheme(args[0]);
                Console.Write(json);
            }
        }
    }
}
