using System;
using log4net;
using Microsoft.Extensions.Logging;
using VkNet;

namespace StatBot.VkApiClient.Logging
{
    public class Log4NetVkApiLoggerAdapter : ILogger<VkApi>, IDisposable
    {
        private readonly ILog logger;

        public Log4NetVkApiLoggerAdapter()
        {
            logger = LogManager.GetLogger(nameof(VkApi));
        }

        public void Dispose()
        {
        }

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            logger.Info(new {logLevel, eventId, state, exception});
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logger.IsInfoEnabled;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new Log4NetVkApiLoggerAdapter();
        }
    }
}