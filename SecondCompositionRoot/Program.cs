using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApp;

namespace SecondCompositionRoot
{
    public class Program
    {
        static void Main(string[] args)
        {
            MoveToProperDirectory();
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices(services => services.AddSingleton<INonUiDependencyRegistry>(
                    provider => new NonUiDependencyRegistry()))
                .Build()
                .Run();
        }

        private static void MoveToProperDirectory()
        {
            if (IsWwwrootPresent()) return;
            
            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            while (true)
            {
                var webAbbDirectory = Path.Combine(parentDirectory, nameof(WebApp));
                
                if (Directory.Exists(webAbbDirectory))
                {
                    Directory.SetCurrentDirectory(webAbbDirectory);
                    return;
                }

                parentDirectory = Directory.GetParent(parentDirectory).FullName;
            }
        }

        private static bool IsWwwrootPresent()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var assumedWwwrootPath = Path.Combine(currentPath, "wwwroot");
            return Directory.Exists(assumedWwwrootPath);
        }
    }
}