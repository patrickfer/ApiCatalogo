
using System.ComponentModel;

namespace ApiCatalogo.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            WriteTextToFile(message);
        }

        private void WriteTextToFile(string message)
        {
            string logFilePath = @"C:\Users\pbrito.MRM\Documents\ProjetosPessoais\ApiCatalogo\Logs\ApiCatalogo_Log.txt";

            using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
            {
                try
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}