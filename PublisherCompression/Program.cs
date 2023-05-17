// See https://aka.ms/new-console-template for more information

using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PublisherCompression;
using PublisherCompression.DataGenerator;
using System.Diagnostics;

// Deliberately not using args or making it configurable as the application is mostly run once and get the results.
// Easier to update the code once, get the result and modify.

// Setup gRPC client telemetry so that we can get the compressed request size over the wire.
var tracerProvider = Sdk.CreateTracerProviderBuilder()
.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(PublishingHelper.Source.Name))
.AddSource(PublishingHelper.Source.Name)
.AddSource("*")
.AddGrpcClientInstrumentation(options =>
{
   options.Enrich = AddEnrichment;
})
.SetErrorStatusOnException()
.Build();

static void AddEnrichment(Activity activity, string eventName, object rawObject)
{
    if (eventName == "OnStartActivity" && rawObject is HttpRequestMessage httpRequest)
    {
        // Temporary hack to use sync over async to get the size of the compressed payload.
        Task.Run(async () => await GetCompressedPayloadSize(activity, httpRequest));        
    }
}

static async Task GetCompressedPayloadSize(Activity activity, HttpRequestMessage httpRequest)
{
    var size = (await httpRequest.Content.ReadAsByteArrayAsync()).Length;
    activity?.SetTag("compressed-payload", size);
    PublishingHelper.Logger.Log($"Compressed request size in bytes: {size}");
    Console.WriteLine($"Compressed request size in bytes: {size}");
}

var delayInMilliSeconds = 5 * 60 * 1000;

// Analyze CPU by publishing Realistic, SemiRandom data of size 500 bytes at the frequency interval of 10,000 and 100,000.
var sizeArray = new int[] { 500 };
var frequencyArray = new int[] { 10_000, 100_000 };

var options = new Options
{
    Logger = PublishingHelper.Logger,
    MessagePattern = MessagePattern.Repeated,
    MessageType = MessageType.Synthetic,
    SizeFilter = SizeFilter.Equal,
};

foreach (var frequency in frequencyArray)
{
    foreach (var size in sizeArray)
    {
        Console.WriteLine($"Size:{size}, Frequency:{frequency} Without compression.");
        var realisticOptions = options.WithFrequency(frequency).WithMessageType(MessageType.Realistic).WithMessagePattern(MessagePattern.SemiRandom).WithMessageSizeEqual(size);
        await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticOptions);
        await Task.Delay(delayInMilliSeconds);

        Console.WriteLine($"Size:{size}, Frequency:{frequency} With compression.");
        await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticOptions.WithCompression(true));
        await Task.Delay(delayInMilliSeconds);
    }
}

Console.WriteLine("CPU Analysis done....");
Console.WriteLine("Starting Bandwidth saving analysis.");
// Analyze bandwidth saving by publishing Realistic - Repeated, SemiRandom and Random data of different sizes with and without compression.
options = new Options
{
    Logger = PublishingHelper.Logger,
    MessagePattern = MessagePattern.Repeated,
    MessageType = MessageType.Realistic,
    SizeFilter = SizeFilter.GreaterThanOrEqual,
    MessageFilterSize = 100,
    NumberOfMessages = 1,
    IntervalInMilliseconds = 0, // No delay between multiple iterations.
    TotalDuration = TimeSpan.FromSeconds(30),
    EnableCompression = true
};
var patterns = new MessagePattern[] { MessagePattern.Repeated, MessagePattern.SemiRandom, MessagePattern.Random };

foreach (var pattern in patterns)
{
    var realOptions = options.WithMessagePattern(pattern);
    await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realOptions);
}

Console.WriteLine("All done.");
Console.ReadLine();