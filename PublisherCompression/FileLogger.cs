using Microsoft.Extensions.Logging;

namespace PublisherCompression;

public class FileLogger : ILogger
{
    private readonly string _filePath;
    private readonly object _lock = new();

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log(string message)
    {
        var formatter = new Func<string, Exception, string>((msg, ex) => msg);
        Log(LogLevel.Information, default, message, null, formatter);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        lock (_lock)
        {
            try
            {
                using var writer = new StreamWriter(_filePath, true);
                var message = formatter(state, exception);
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{logLevel}] {message}");
            }
            catch
            {
                // Ignore.
            }
        }
    }
}

