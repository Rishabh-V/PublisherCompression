// See https://aka.ms/new-console-template for more information

using PublisherCompression;
using PublisherCompression.DataGenerator;

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\rishabhverma\Downloads\cloudmigrationassistant-ae100c039e3a.json");

var logger = new FileLogger("Performance.txt");

await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Synthetic, MessagePattern.Repeated, false, 10);
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Synthetic, MessagePattern.Repeated, true, 10);
//--------------------------------------------------------------------------------
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Synthetic, MessagePattern.SemiRandom, false, 10);
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Synthetic, MessagePattern.SemiRandom, true, 10);
//--------------------------------------------------------------------------------
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Synthetic, MessagePattern.Random, false, 10);
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Synthetic, MessagePattern.Random, true, 10);
//---------------------------------------------------------------------------------
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Realistic, MessagePattern.Repeated, false, 10);
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Realistic, MessagePattern.Repeated, true, 10);
//--------------------------------------------------------------------------------
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Realistic, MessagePattern.SemiRandom, false, 10);
await Task.Delay(2 * 60 * 1000);
await PublishingHelper.RunIteration(PublishingHelper.ExecuteTestSuiteAsync, logger, MessageType.Realistic, MessagePattern.SemiRandom, true, 10);
//--------------------------------------------------------------------------------

Console.WriteLine("All done");
Console.ReadLine();
