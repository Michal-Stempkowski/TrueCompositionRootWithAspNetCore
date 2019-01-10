using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            throw new InvalidOperationException("Use Composition root instead");
        }

        public static void ServerEntryPoint(INonUiDependencyRegistry nonUiDependencyRegistry, string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            CreateWebHostBuilder(args)
                .ConfigureServices(services => services.AddSingleton(provider => nonUiDependencyRegistry))
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}