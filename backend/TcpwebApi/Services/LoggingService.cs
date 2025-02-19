using Serilog;
using TcpWebApi.Contracts;

namespace TcpWebApi.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly Serilog.ILogger _logger;

        public LoggingService()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/logsof.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogInfo(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void LogWarn(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
        }
    }
}
