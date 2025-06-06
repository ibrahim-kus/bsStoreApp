﻿using NLog;
using Services.Contract;

namespace Services
{
    public class LoggerManager : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger(); //Concreate(somutlaştırma)
        public void LogDebug(string message) => logger.Debug(message);    

        public void LogError(string message) => logger.Error(message);

        public void LogInfo(string message) => logger.Info(message);
 
        public void LogWarning(string message) => logger.Warn(message);

    }
}
