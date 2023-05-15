﻿using Google.Api.Gax.Grpc;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PublisherCompression;

internal static class PublishingHelper
{
    private static string s_folder = Path.Combine(AppContext.BaseDirectory, "Test Data");
    private static List<string> s_testFiles = Directory.EnumerateFiles(s_folder, "*.txt", SearchOption.AllDirectories).ToList();
    private static ConcurrentDictionary<(string MessageType, string MessagePattern, int Size), string> s_dictionary = new();
    private static GrpcAdapter s_grpcAdapter;
    
    static PublishingHelper()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddProvider(new FileLoggerProvider("grpc.txt")).AddConsole().SetMinimumLevel(LogLevel.Trace));
        s_grpcAdapter = GrpcNetClientAdapter.Default
            .WithAdditionalOptions(options => options.LoggerFactory = loggerFactory);

        // File name is of the format <message type>_<message pattern>_<size in bytes>.txt
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

    internal static async Task<int> PublishBatchMessagesAsync(IEnumerable<string> messageTexts, bool enableCompression = false, string projectId = "cloudmigrationassistant", string topicId = "dependency_injection")
    {
        TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
        var customSettings = new PublisherClient.Settings
        {
            EnableCompression = enableCompression,
        };

        PublisherClient publisher = await new PublisherClientBuilder
        {
            TopicName = topicName,
            Settings = customSettings,
            GrpcAdapter = s_grpcAdapter,
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

    internal static async Task ExecuteTestSuiteAsync(MessageType messageType = MessageType.Synthetic, MessagePattern pattern = MessagePattern.Repeated, bool enableCompression = false, int numberOfMessages = 10, int messageFilterSize = 100, SizeFilter filter = SizeFilter.GreaterThanOrEqual)
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

            Console.WriteLine($"Compression: {enableCompression}, Number of Messages: {numberOfMessages}, MessageType: {msgType}, MessagePattern: {msgPattern}, Size: {size} bytes");

            var messageList = Enumerable.Repeat<string>(message, numberOfMessages);
            await PublishBatchMessagesAsync(messageList, enableCompression);
        }
    }

    internal static async Task RunIteration(Func<MessageType, MessagePattern, bool, int, int, SizeFilter, Task> action, Options options)
    {
        var logger = options.Logger;
        var messageType = options.MessageType;
        var pattern = options.MessagePattern;
        var enableCompression = options.EnableCompression;
        var numberOfMessages = options.NumberOfMessages;
        var numberOfIterations = options.NumberOfIterations;
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
            await Task.Delay(intervalInMilliseconds);
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
            NumberOfIterations = this.NumberOfIterations,
            NumberOfMessages = this.NumberOfMessages,
            SizeFilter = this.SizeFilter,
            TotalDuration = this.TotalDuration,
            IntervalInMilliseconds = this.IntervalInMilliseconds
        };

    public ILogger Logger { get; set; }

    public TimeSpan TotalDuration { get; set; } = TimeSpan.FromHours(1);

    public int IntervalInMilliseconds { get; set; } = 1000;

    public MessageType MessageType { get; set; } = MessageType.Synthetic;

    public MessagePattern MessagePattern { get; set; } = MessagePattern.Repeated;

    public bool EnableCompression { get; set; } = false;

    public int NumberOfMessages { get; set; } = 10;

    public int NumberOfIterations { get; set; } = 200;

    public int MessageFilterSize { get; set; } = 100;

    public SizeFilter SizeFilter { get; set; } = SizeFilter.GreaterThanOrEqual;
}