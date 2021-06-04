using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger _logger;

        public LoggerManager(string rule = "appLoggerRule")
        {
            _logger = LogManager.GetLogger(rule);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, object data)
        {
            _logger.Debug(message, data);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, object ex)
        {
            _logger.Error(message, ex);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, object data)
        {
            _logger.Info(message, data);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(string message, object data)
        {
            _logger.Warn(message, data);
        }
    }
}
