using Google.Api.Gax;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using PublisherCompression.DataGenerator;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace PublisherCompression;

internal static class PublishingHelper
{
    private static string s_folder = Path.Combine(AppContext.BaseDirectory, "Test Data");
    private static List<string> s_testFiles = Directory.EnumerateFiles(s_folder, "*.txt", SearchOption.AllDirectories).ToList();
    private static ConcurrentDictionary<(string MessageType, string MessagePattern, int Size), string> s_dictionary = new();
    
    internal static readonly AssemblyName s_assemblyName = Assembly.GetEntryAssembly().GetName();
    internal static readonly string s_serviceName = s_assemblyName.Name;
    internal static readonly string s_version = s_assemblyName.Version.ToString();
    internal static readonly ActivitySource Source = new(s_serviceName, s_version);
    internal static readonly FileLogger Logger = new FileLogger("Performance.txt");

    static PublishingHelper()
    {     
        // File name is of the format <message type>_<message pattern>_<size in bytes>.txt
        // Note: The following size (in bytes) text file exists as that is what we need.
        // 100, 500, 1000, 5000, 10000, 50000, 100000, 500000, 1000000, 3400000 
        foreach (var file in s_testFiles)
        {
            var fileInfo = new FileInfo(file);
            var name = fileInfo.Name;
            var fragments = name.Split('_');
            var messageType = fragments[0];
            var messagePattern = fragments[1];
            var sizeInBytes = fragments[2].Replace(".txt", "");
            var size = int.Parse(sizeInBytes);
            var message = File.ReadAllText(file);
            s_dictionary.GetOrAdd((messageType, messagePattern, size), (k) => message);
        }

        Console.WriteLine($"Total {s_dictionary.Count} different messages added to dictionary for testing.");
    }

    internal static async Task<int> PublishCompressedMessagesAsync(IEnumerable<string> messageTexts, bool enableCompression = false, string projectId = "cloudmigrationassistant", string topicId = "dependency_injection")
    {
        TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
        var customSettings = new PublisherClient.Settings
        {
            EnableCompression = enableCompression,
            CompressionBytesThreshold = 240,
            BatchingSettings = new BatchingSettings(100L, null, null),
        };

        PublisherClient publisher = await new PublisherClientBuilder
        {
            TopicName = topicName,
            Settings = customSettings,
        }.BuildAsync();

        int publishedMessageCount = 0;
        var publishTasks = messageTexts.Select(async text =>
        {
            try
            {
                string message = await publisher.PublishAsync(text);
                Interlocked.Increment(ref publishedMessageCount);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred when publishing message {text}: {exception}");
            }
        });
        await Task.WhenAll(publishTasks);
        return publishedMessageCount;
    }

    internal static async Task ExecuteTestSuiteAsync(MessageType messageType = MessageType.Synthetic, MessagePattern pattern = MessagePattern.Repeated, bool enableCompression = false, int numberOfMessages = 100, int messageFilterSize = 100, SizeFilter filter = SizeFilter.GreaterThanOrEqual)
    {
        var filteredMessages = filter switch
        {
            SizeFilter.GreaterThanOrEqual => s_dictionary.Where(j => j.Key.Size >= messageFilterSize),
            SizeFilter.LessThanOrEqual => s_dictionary.Where(j => j.Key.Size <= messageFilterSize),
            SizeFilter.Equal => s_dictionary.Where(j => j.Key.Size == messageFilterSize),
            _ => s_dictionary.Where(j => j.Key.Size >= messageFilterSize),
        };

        foreach (KeyValuePair<(string MessageType, string MessagePattern, int Size), string> item in filteredMessages
            .Where(j => j.Key.MessageType == messageType.ToString() && j.Key.MessagePattern == pattern.ToString())
            .OrderBy(j => j.Key.Size).ThenBy(j => j.Key.MessageType).ThenBy(j => j.Key.MessagePattern))
        {
            var key = item.Key;
            var msgType = key.MessageType;
            var msgPattern = key.MessagePattern;
            var size = key.Size;
            var message = item.Value;
            var actualSize = System.Text.Encoding.UTF8.GetBytes(message).Length;
            Console.WriteLine($"Compression: {enableCompression}, Number of Messages: {numberOfMessages}, MessageType: {msgType}, MessagePattern: {msgPattern}");

            var messageList = Enumerable.Repeat<string>(message, numberOfMessages);
            await PublishCompressedMessagesAsync(messageList, enableCompression);
        }
    }

    internal static async Task RunIteration(Func<MessageType, MessagePattern, bool, int, int, SizeFilter, Task> action, Options options)
    {
        var logger = options.Logger;
        var messageType = options.MessageType;
        var pattern = options.MessagePattern;
        var enableCompression = options.EnableCompression;
        var numberOfMessages = options.NumberOfMessages;
        var messageFilterSize = options.MessageFilterSize;
        var sizeFilter = options.SizeFilter;
        var duration = options.TotalDuration;
        var intervalInMilliseconds = options.IntervalInMilliseconds;

        logger.LogInformation($"MessageSize: {sizeFilter}{messageFilterSize} bytes,  MessageType: {messageType}, MessagePattern: {pattern}, Compression: {enableCompression}, NumberOfMessages: {numberOfMessages}");
        logger.LogInformation($"Start Time: {DateTime.Now.ToLocalTime()}");
        Stopwatch stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed < duration)
        {
            await action(messageType, pattern, enableCompression, numberOfMessages, messageFilterSize, sizeFilter);
            if (intervalInMilliseconds != 0)
            {
                await Task.Delay(intervalInMilliseconds);
            }
        }

        stopwatch.Stop();
        logger.LogInformation($"End Time: {DateTime.Now.ToLocalTime()}");
    }    
}

internal sealed class Options
{
    internal static Options Default => new Options();

    internal static Options DefaultWithLogger(ILogger logger) => Default.WithLogger(logger);

    internal Options WithLogger(ILogger logger)
    {
        var clone = Clone();
        clone.Logger = logger;
        return clone;
    }

    internal Options WithCompression(bool enableCompression)
    {
        var clone = Clone();
        clone.EnableCompression = enableCompression;
        return clone;
    }

    internal Options WithMessageType(MessageType messageType)
    {
        var clone = Clone();
        clone.MessageType = messageType;
        return clone;
    }

    internal Options WithFrequency(int intervalInMilliseconds)
    {
        var clone = Clone();
        clone.IntervalInMilliseconds = intervalInMilliseconds;
        return clone;
    }

    internal Options WithMessagePattern(MessagePattern messagePattern)
    {
        var clone = Clone();
        clone.MessagePattern = messagePattern;
        return clone;
    }

    internal Options WithMessageSizeGreater(int filterSize)
    {
        var clone = Clone();
        clone.MessageFilterSize = filterSize;
        clone.SizeFilter = SizeFilter.GreaterThanOrEqual;
        return clone;
    }

    internal Options WithMessageSizeEqual(int filterSize)
    {
        var clone = Clone();
        clone.MessageFilterSize = filterSize;
        clone.SizeFilter = SizeFilter.Equal;
        return clone;
    }

    internal Options WithMessageSizeLesser(int filterSize)
    {
        var clone = Clone();
        clone.MessageFilterSize = filterSize;
        clone.SizeFilter = SizeFilter.LessThanOrEqual;
        return clone;
    }

    internal Options Clone() =>
        new()
        {
            EnableCompression = this.EnableCompression,
            Logger = this.Logger,
            MessageFilterSize = this.MessageFilterSize,
            MessagePattern = this.MessagePattern,
            MessageType = this.MessageType,
            NumberOfMessages = this.NumberOfMessages,
            SizeFilter = this.SizeFilter,
            TotalDuration = this.TotalDuration,
            IntervalInMilliseconds = this.IntervalInMilliseconds
        };

    public ILogger Logger { get; set; }

    public TimeSpan TotalDuration { get; set; } = TimeSpan.FromHours(1);

    public int IntervalInMilliseconds { get; set; } = 100000;

    public MessageType MessageType { get; set; } = MessageType.Synthetic;

    public MessagePattern MessagePattern { get; set; } = MessagePattern.Repeated;

    public bool EnableCompression { get; set; } = false;

    public int NumberOfMessages { get; set; } = 100;

    public int MessageFilterSize { get; set; } = 500;

    public SizeFilter SizeFilter { get; set; } = SizeFilter.GreaterThanOrEqual;
}