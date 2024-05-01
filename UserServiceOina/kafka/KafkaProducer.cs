using System.Text.Json;
using Confluent.Kafka;

namespace UserServiceOina.kafka;

public class KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger)
{
    private readonly ProducerConfig _config = new()
        { BootstrapServers = configuration.GetValue<string>("KafkaSettings:BootstrapServers") };

    public async Task SendMessageAsync(string topic, Object message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        try
        {
            var result = await producer.ProduceAsync(topic,
                new Message<Null, string> { Value = JsonSerializer.Serialize(message) });
            logger.LogInformation($"Message sent to {result.TopicPartitionOffset}");
        }
        catch (ProduceException<Null, string> e)
        {
            logger.LogInformation($"Failed to deliver message: {e.Message} [{e.Error.Code}]");
        }
    }
}