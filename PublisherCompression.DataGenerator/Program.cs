// See https://aka.ms/new-console-template for more information

using PublisherCompression.DataGenerator;

var messageTypes = Enum.GetValues(typeof(MessageType));
var messagePatterns = Enum.GetValues(typeof(MessagePattern));

foreach (MessageType messageType in messageTypes)
{
    foreach (MessagePattern messagePattern in messagePatterns)
    {
        // All sizes are in bytes from 100 bytes to 3.4 MB. The size is doubled everytime till 3.4 MB.
        for (int size = 100; size <= 3_400_000; size*=2)
        {
            var publishDataGenerator = new PublishDataGenerator(messageType, messagePattern, size);
            var data = publishDataGenerator.Generate();
            var fileName = $"{messageType}_{messagePattern}_{size}.txt";
            using var writer = new StreamWriter(fileName);
            writer.Write(data);
        }
    }
}

Console.WriteLine("All done");
Console.ReadLine();