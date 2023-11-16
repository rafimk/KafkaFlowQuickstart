using Microsoft.Extensions.DependencyInjection;
using KafkaFlow.Producers;
using KafkaFlow.Serializer;
using KafkaFlow;
using Producer;
using Confluent.Kafka;

var services = new ServiceCollection();

const string topicName = "sample-topic";
const string producerName = "say-hello";

services.AddKafka(
    kafka => kafka
        .UseConsoleLog()
        .AddCluster(
            cluster => cluster
                .WithBrokers(new[] { "localhost:9092" })
                .CreateTopicIfNotExists(topicName, 1, 1)
                .AddProducer(
                    producerName,
                    producer => producer
                    .WithCompression(CompressionType.Gzip)
                    .WithAcks(KafkaFlow.Acks.All)
                        .DefaultTopic(topicName)
                        .AddMiddlewares(m =>
                            m.AddSerializer<JsonCoreSerializer>()
                        )
                )
        )
);

var serviceProvider = services.BuildServiceProvider();

var producer = serviceProvider
    .GetRequiredService<IProducerAccessor>()
    .GetProducer(producerName);

await producer.ProduceAsync(
    topicName,
    Guid.NewGuid().ToString(),
    new HelloMessage { Text = "Hello!" });


Console.WriteLine("Message sent!");