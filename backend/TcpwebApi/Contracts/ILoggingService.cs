using System;

namespace TcpWebApi.Contracts
{
    public interface ILoggingService
    {
        void LogInformation(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
    }
}
