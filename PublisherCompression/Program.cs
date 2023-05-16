// See https://aka.ms/new-console-template for more information

using PublisherCompression;
using PublisherCompression.DataGenerator;

var logger = new FileLogger("Performance.txt");
var delayInMilliSeconds = 5 * 60 * 1000;

// Analyze CPU by publishing Realistic, SemiRandom data of size 500 bytes at the frequency interval of 10,000 and 100,000.
var sizeArray = new int[] { 500 };
var frequencyArray = new int[] { 10_000, 100_000 };

var options = new Options
{
    Logger = logger,
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
// Analyze bandwidth saving by publishing Realistic - Repeated, SemiRandom and Random data of size 500 bytes for 1 hour with and without compression.
options = new Options
{
    Logger = logger,
    MessagePattern = MessagePattern.Repeated,
    MessageType = MessageType.Synthetic,
    SizeFilter = SizeFilter.Equal,
    MessageFilterSize = 500
};

Console.WriteLine("Without compression.");
var realisticRepatedOptions = options.WithMessageType(MessageType.Realistic).WithMessagePattern(MessagePattern.Repeated);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticRepatedOptions);
await Task.Delay(delayInMilliSeconds);

Console.WriteLine("With compression.");
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticRepatedOptions.WithCompression(true));
await Task.Delay(delayInMilliSeconds);

Console.WriteLine("Without compression.");
var realisticSemiRandomOptions = options.WithMessageType(MessageType.Realistic).WithMessagePattern(MessagePattern.SemiRandom);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticSemiRandomOptions);
await Task.Delay(delayInMilliSeconds);

Console.WriteLine("With compression.");
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticSemiRandomOptions.WithCompression(true));
await Task.Delay(delayInMilliSeconds);

Console.WriteLine("Without compression.");
var realisticRandomOptions = options.WithMessageType(MessageType.Realistic).WithMessagePattern(MessagePattern.SemiRandom);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticRandomOptions);
await Task.Delay(delayInMilliSeconds);

Console.WriteLine("With compression.");
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticRandomOptions.WithCompression(true));

Console.WriteLine("All done.");
Console.ReadLine();
