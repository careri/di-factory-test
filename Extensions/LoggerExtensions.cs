namespace Microsoft.Extensions.Logging
{
    public static class LoggerExtensions
    {
        public static void LogThreadInformation(this ILogger logger, string msg)
        {
            logger.LogInformation($"[TID:{Environment.CurrentManagedThreadId}] {msg}");
        }
    }
}