using Google.Api.Gax.Grpc;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using PublisherCompression.DataGenerator;
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

    internal static async Task<int> PublishBatchMessagesAsync(IEnumerable<string> messageTexts, bool enableCompression = false, string projectId ="cloudmigrationassistant", string topicId= "dependency_injection")
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

    internal static async Task ExecuteTestSuiteAsync(MessageType messageType = MessageType.Synthetic, MessagePattern pattern = MessagePattern.Repeated, bool enableCompression = false, int numberOfMessages = 10)
    {
        foreach (KeyValuePair<(string MessageType, string MessagePattern, int Size), string> item in s_dictionary
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
        
    internal static async Task RunIteration(
        Func<MessageType, MessagePattern, bool, int, Task> action, 
        ILogger logger, 
        MessageType messageType = MessageType.Synthetic, 
        MessagePattern pattern = MessagePattern.Repeated, 
        bool enableCompression = false, 
        int numberOfMessages = 10, 
        int numberOfIterations = 100)
    {
        logger.LogInformation($"MessageType: {messageType}, MessagePattern: {pattern}, Compression: {enableCompression}, NumberOfMessages: {numberOfMessages}, NumberOfIterations: {numberOfIterations}");
        logger.LogInformation($"Start Time: {DateTime.Now.ToLocalTime()}");
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < numberOfIterations; i++)
        {
            Console.WriteLine($"Iteration number = {i}");
            await action(messageType, pattern, enableCompression, numberOfMessages);
        }
        stopwatch.Stop();
        logger.LogInformation($"Took {stopwatch.ElapsedMilliseconds} milliseconds.");
        logger.LogInformation($"End Time: {DateTime.Now.ToLocalTime()}");
    }
}