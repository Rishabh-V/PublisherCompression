// See https://aka.ms/new-console-template for more information

using PublisherCompression.DataGenerator;

var messageTypes = Enum.GetValues(typeof(MessageType));
var messagePatterns = Enum.GetValues(typeof(MessagePattern));

var sizes = new int[] { 100, 500, 1000, 5000, 10000, 50000, 100000, 500000, 1000000, 3400000 };

foreach (MessageType messageType in messageTypes)
{
    foreach (MessagePattern messagePattern in messagePatterns)
    {
        foreach (var sizeInBytes in sizes)
        {
            Console.WriteLine($"Processing {messageType} {messagePattern} {sizeInBytes} bytes");
            var publishDataGenerator = new PublishDataGenerator(messageType, messagePattern, sizeInBytes);
            var data = publishDataGenerator.Generate();
            var fileName = $"{messageType}_{messagePattern}_{sizeInBytes}.txt";
            using var writer = new StreamWriter(fileName);
            writer.Write(data);
        }
    }
}

Console.WriteLine("All done");
Console.ReadLine();