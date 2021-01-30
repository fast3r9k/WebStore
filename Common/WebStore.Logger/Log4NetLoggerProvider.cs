﻿using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _ConfigurationFile;

        private readonly ConcurrentDictionary<string, Log4NetLogger> _Loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        public Log4NetLoggerProvider(string ConfigurationFile) => _ConfigurationFile = ConfigurationFile;
        public void Dispose() => _Loggers.Clear();

        public ILogger CreateLogger(string Category) => _Loggers.GetOrAdd(
            Category, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_ConfigurationFile);
                return new Log4NetLogger(category, xml["log4Net"]);
            });
    }
}