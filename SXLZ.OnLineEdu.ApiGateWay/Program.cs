using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Com.Ctrip.Framework.Apollo;
using Microsoft.Extensions.Logging;
using Com.Ctrip.Framework.Apollo.Enums;
using System.IO;
using NLog;

namespace SXLZ.OnLineEdu.ApiGateWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(builder => builder //普通方式，一般配置在appsettings.json中
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, true)
                    .AddJsonFile("ocelot.json")
                    .AddEnvironmentVariables()
                    .AddApollo(builder.Build().GetSection("apollo"))
                    .AddDefault(ConfigFileFormat.Xml)
                    .AddDefault(ConfigFileFormat.Json)
                    .AddNamespace("org01.sxzgh")
                    .AddNamespace("gateway12345678", ConfigFileFormat.Json)
                    .AddDefault());
                    webBuilder.UseStartup<Startup>();
                });
    }
}
