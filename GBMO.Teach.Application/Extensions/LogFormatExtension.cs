using GBMO.Teach.Core.Configurations.Logging;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GBMO.Teach.Application.Extensions
{
    public static class LogFormatExtension
    {
        public static void LogInformation<T>(this ILogger logger, T logModel) where T : MainLogFormat
        {
            var jsonLog = JsonSerializer.Serialize(logModel);
            logger.LogInformation(jsonLog);
        }

        public static void LogError<T>(this ILogger logger, T logModel) where T : MainLogFormat
        {
            var jsonLog = JsonSerializer.Serialize(logModel);
            logger.LogError(jsonLog);
        }
    }
}
