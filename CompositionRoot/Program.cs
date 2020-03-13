using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApp;

namespace CompositionRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            MoveToProperDirectory();
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
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