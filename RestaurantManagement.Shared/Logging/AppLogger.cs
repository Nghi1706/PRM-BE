using Microsoft.Extensions.Logging;

namespace RestaurantManagement.Shared.Logging
{
    public static class AppLogger<T>
    {
        private static ILoggerFactory? _loggerFactory;

        public static void ConfigureLoggerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public static ILogger<T> Instance =>
            _loggerFactory?.CreateLogger<T>()
            ?? throw new InvalidOperationException("LoggerFactory is not configured.");
    }
}
