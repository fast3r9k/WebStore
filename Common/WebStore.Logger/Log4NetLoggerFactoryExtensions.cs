using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4Net.config")
        {
            if(ConfigurationFile is null) throw new ArgumentNullException(nameof(ConfigurationFile));

            if (!Path.IsPathRooted(ConfigurationFile))
            {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Failed to determine the library includes the entry point ");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Empty link received");
                ConfigurationFile = Path.Combine(dir, ConfigurationFile);
            }
            Factory.AddProvider(new Log4NetLoggerProvider(ConfigurationFile));
            return Factory;
        }
    }
}