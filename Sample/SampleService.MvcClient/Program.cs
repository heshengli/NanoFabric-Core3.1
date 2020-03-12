﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SampleService.MvcClient
{
    public class Program
    {
        private const string defaultAddress = "http://127.0.0.1:9000";
        private const string addressKey = "serveraddress";

        public static void Main(string[] args)
        {
            var configurationBuilder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddJsonFile("appsettings.Development.json", true, false)
          .AddJsonFile("appsettings.Production.json", true, false)
          .AddEnvironmentVariables()
          .AddCommandLine(args);

            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }
            var hostingconfig = configurationBuilder.Build();
            var url = hostingconfig[addressKey] ?? defaultAddress;

            IWebHostBuilder builder = new WebHostBuilder();
            builder.ConfigureServices(s =>
            {
                s.AddSingleton(builder);
            }).ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            });
            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(hostingconfig)
                .UseIISIntegration()
                .UseUrls(url)
                .UseStartup<Startup>();
            var host = builder.Build();
            host.Run();
        }
    }
}