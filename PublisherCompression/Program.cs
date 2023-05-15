// See https://aka.ms/new-console-template for more information

using PublisherCompression;
using PublisherCompression.DataGenerator;

var logger = new FileLogger("Performance.txt");
var delayInMilliSeconds = 5 * 60 * 1000;

var sizeArray = new int[] { 100, 500, 1000, 16000, 64000, 128000, 256000, 512000, 1024000, 2048000 };
var options = new Options
{
    Logger = logger,
    MessagePattern = MessagePattern.Repeated,
    MessageType = MessageType.Synthetic,
    SizeFilter = SizeFilter.Equal
};

foreach (var size in sizeArray)
{
    var syntheticRepeatedOptions = options.WithMessageType(MessageType.Synthetic).WithMessagePattern(MessagePattern.Repeated).WithMessageSizeEqual(size);
    await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, syntheticRepeatedOptions);
    await Task.Delay(delayInMilliSeconds);
    await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, syntheticRepeatedOptions.WithCompression(true));
    //--------------------------------------------------------------------------------

    var realisticRepeatedOptions = syntheticRepeatedOptions.WithMessageType(MessageType.Realistic).WithMessagePattern(MessagePattern.Repeated);
    await Task.Delay(delayInMilliSeconds);
    await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticRepeatedOptions);
    await Task.Delay(delayInMilliSeconds);
    await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, realisticRepeatedOptions.WithCompression(true));
}

Console.WriteLine("All done....");
Console.ReadLine();
