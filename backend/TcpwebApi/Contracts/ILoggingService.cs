using System;

namespace TcpWebApi.Contracts
{
    public interface ILoggingService
    {
        void LogInfo(string message, params object[] args);
        void LogWarn(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
    }
}
