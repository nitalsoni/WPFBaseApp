namespace LoggerService
{
    public interface ILoggerManager
    {
        void Info(string message);
        void Info(string message, object data);

        void Warn(string message);

        void Warn(string message, object data);

        void Debug(string message);

        void Debug(string message, object data);

        void Error(string message);

        void Error(string message, object ex);
    }
}
