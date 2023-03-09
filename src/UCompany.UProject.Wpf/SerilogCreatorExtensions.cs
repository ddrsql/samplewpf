using Avalonia.Controls.Shapes;
using Avalonia.OpenGL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCompany.UProject.Wpf
{
    public static class SerilogCreatorExtensions
    {
        public static Serilog.ILogger logger;
        public static Serilog.Core.Logger CreateSerilog(this IConfigurationRoot configuration)
        {
            return BuildSerilogConfiguration(configuration).CreateLogger();
        }
        private static LoggerConfiguration BuildSerilogConfiguration(IConfiguration configuration)
        {
            // 默认读取 configuration 中 "Serilog" 节点下的配置
            var loggerConfiguration = new LoggerConfiguration();
            SetSerilogConfiguration(loggerConfiguration, configuration);
            return loggerConfiguration;
        }
        public static void SetSerilogConfiguration(LoggerConfiguration loggerConfiguration, IConfiguration configuration)
        {
            // 默认读取 configuration 中 "Serilog" 节点下的配置
            loggerConfiguration.ReadFrom.Configuration(configuration)
                .Enrich.WithExceptionDetails()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })
                )
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()//最小记录级别
                .WriteTo.Console();
            var logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Logs", "logs.txt");
            loggerConfiguration.WriteTo.File(logPath, rollingInterval: RollingInterval.Day, shared: true);
        }
        public static IServiceCollection CreateSeriLog(this IServiceCollection collection, IConfigurationRoot configuration)
        {
            if (logger == null)
            {
                logger = BuildSerilogConfiguration(configuration).CreateLogger();
            }
            return collection;
        }

        public static IServiceCollection UseSeriLog(this IServiceCollection collection, Serilog.ILogger logger = null,
            bool dispose = false, LoggerProviderCollection providers = null)
        {
            if (providers != null)
            {
                collection.AddSingleton<ILoggerFactory>(services =>
                {
                    var factory = new SerilogLoggerFactory(logger, dispose, providers);

                    foreach (var provider in services.GetServices<ILoggerProvider>())
                        factory.AddProvider(provider);

                    return factory;
                });
            }
            else
            {
                collection.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(logger, dispose));
            }
            ConfigureServices(collection, logger);

            return collection;
        }

        static void ConfigureServices(IServiceCollection collection, Serilog.ILogger logger)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            if (logger != null)
            {
                collection.AddSingleton(logger);
            }
        }
    }
}
