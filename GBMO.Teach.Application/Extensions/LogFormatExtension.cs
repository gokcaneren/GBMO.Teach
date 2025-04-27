using GBMO.Teach.Core.Configurations.Logging;
using Serilog;
using System.Text.Json;

namespace GBMO.Teach.Application.Extensions
{
    public static class LogFormatExtension
    {
        public static void LogInformation<T>(this ILogger logger, T logModel) where T : MainLogFormat
        {
            var jsonLog = JsonSerializer.Serialize(logModel);
            logger.ForContext(new LogEnricher<T>(logModel)).Information(jsonLog);
        }

        public static void LogError<T>(this ILogger logger, T logModel) where T : MainLogFormat
        {
            var jsonLog = JsonSerializer.Serialize(logModel);
            logger.ForContext(new LogEnricher<T>(logModel)).Error(jsonLog);
        }
    }
}
