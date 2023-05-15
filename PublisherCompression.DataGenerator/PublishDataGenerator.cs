using Google.Cloud.PubSub.Compression.Thrift;
using System.Text;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace PublisherCompression.DataGenerator;

public class PublishDataGenerator
{
    private readonly MessageType _messageType;
    private readonly MessagePattern _messagePattern;
    private readonly int _size;
    private readonly ThriftObjectGenerator _thriftObjectGenerator;

    public PublishDataGenerator(MessageType messageType, MessagePattern messagePattern, int size)
    {
        _messageType = messageType;
        _messagePattern = messagePattern;
        _size = size;
        _thriftObjectGenerator = new ThriftObjectGenerator();
    }

    public string Generate()
    {
        return (_messageType, _messagePattern) switch
        {
            (MessageType.Realistic, MessagePattern.Repeated) => GenerateRepeatedRealistic(),
            (MessageType.Realistic, MessagePattern.SemiRandom) => GenerateSemiRandomRealistic(),
            (MessageType.Realistic, MessagePattern.Random) => GenerateRepeatedRealistic(),
            (MessageType.Synthetic, MessagePattern.Repeated) => GenerateRepeatedSynthetic(),
            (MessageType.Synthetic, MessagePattern.SemiRandom) => GenerateSemiRandomSynthetic(),
            (MessageType.Synthetic, MessagePattern.Random) => GenerateRandomSynthetic(),
            _ => throw new NotImplementedException(),
        };
    }

    private string GenerateRandomSynthetic()
    {
        Random random = new();
        byte[] bytes = new byte[_size];
        random.NextBytes(bytes);
        return Encoding.UTF8.GetString(bytes);
    }

    private string GenerateSemiRandomSynthetic()
    {
        Random random = new();
        List<string> stringList = new();
        int listSize = Math.Max(10, _size / 15);
        int byteStringSize = 15;
        for (int i = 0; i < listSize; i++)
        {
            byte[] bytes = new byte[byteStringSize];
            random.NextBytes(bytes);
            stringList.Add(Encoding.UTF8.GetString(bytes));
        }

        var resultBuilder = new StringBuilder();
        for (int i = 0; i < _size / byteStringSize + 1; i++)
        {
            int randomIndex = random.Next(listSize);
            resultBuilder.Append(stringList[randomIndex]);
        }

        return resultBuilder.ToString();
    }

    private string GenerateRepeatedSynthetic()
    {
        string token = "Hello World!";
        var messageBuilder = new StringBuilder();
        int tokenSize = token.Length;
        var limit = _size / tokenSize;
        for (int i = 0; i < limit + 1; i++)
        {
            messageBuilder.Append(token);
        }

        return messageBuilder.ToString();
    }

    private string GenerateSemiRandomRealistic()
    {
        StringBuilder resultBuilder = new StringBuilder();
        try
        {
            Twitter twitter = new()
            {
                Spaces = new List<Space>()
            };
            
            while (resultBuilder.Length < _size)
            {
                twitter.Spaces.Add(_thriftObjectGenerator.GenerateSpace());
                resultBuilder.Append(Serialize(twitter));
            }
        }
        catch (TTransportException e)
        {
            Console.WriteLine(e);
        }
        return resultBuilder.ToString();
    }

    private static byte[] Serialize(TBase obj)
    {
        var result = SerializeAsync(obj);
        return result.Result;
    }

    private static T Deserialize<T>(byte[] data) where T : TBase, new()
    {
        var result = DeserializeAsync<T>(data);
        return result.Result;
    }

    private static async Task<byte[]> SerializeAsync(TBase obj)
    {
        using var stream = new MemoryStream();
        using TProtocol tProtocol = new TBinaryProtocol(new TStreamTransport(stream, stream, null));
        await obj.WriteAsync(tProtocol);
        return stream.ToArray();
    }

    private static async Task<T> DeserializeAsync<T>(byte[] data) where T : TBase, new()
    {
        T result = new();
        using var buffer = new TMemoryBufferTransport(data, null);
        using TProtocol tProtocol = new TBinaryProtocol(buffer);
        await result.ReadAsync(tProtocol);
        return result;
    }

    private string GenerateRepeatedRealistic()
    {
        var resultBuilder = new StringBuilder();
        try
        {
            PhoneBook phoneBook = new()
            {
                People = new List<Person>()
            };
            
            while (resultBuilder.Length < _size)
            {
                phoneBook.People.Add(_thriftObjectGenerator.GeneratePerson());
                var serializedBytes = Serialize(phoneBook);
                resultBuilder.Append(Encoding.UTF8.GetString(serializedBytes));
            }
        }
        catch (TTransportException e)
        {
            Console.WriteLine(e);
        }

        return resultBuilder.ToString();
    }
}

public enum MessageType
{
    Synthetic = 0,
    Realistic
}

public enum MessagePattern
{
    Repeated = 0,
    SemiRandom,
    Random
}

public enum SizeFilter
{
    GreaterThanOrEqual = 0,
    LessThanOrEqual,
    Equal
}
