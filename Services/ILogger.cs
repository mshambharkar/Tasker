namespace Tasker.Services
{
    interface ILogger
    {
        void LogInfo(string format, params object[] param);
        void LogInfo(string message);
        void LogWarning(string format, params object[] param);
        void LogWarning(string message);
        void LogError(string format, params object[] param);
        void LogError(string message);
        void SetLoggingLevel(eLoggingLevel level);
    }
    enum eLoggingLevel
    {
        Info,
        Warning,
        Error
    }
}
