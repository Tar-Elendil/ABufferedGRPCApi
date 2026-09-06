using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace Demo.GRPC.Endpoint.Services;

public class MovementsPublisher : IPublishMessagesAsync<MovementSaveRequest>, IDisposable
{
    private readonly CachedSchemaRegistryClient SchemRegistry;
    private readonly IProducer<string, MovementSaveRequest> Producer;

    // Hard code topics for now
    // Should have configuration builder
    private readonly Dictionary<Type, string> Topics = new()
    {
        { typeof(MovementSaveRequest), "" }
    };

    public MovementsPublisher(ProducerConfig config, SchemaRegistryConfig schemaRegistryConfig)
    {
        SchemRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
        Producer = new ProducerBuilder<string, MovementSaveRequest>(config)
            .SetValueSerializer(new ProtobufSerializer<MovementSaveRequest>(SchemRegistry))
            .Build();
    }

    public Task<bool> Publish(MovementSaveRequest message, string key)
    {
        if (!Topics.TryGetValue(typeof(MovementSaveRequest), out var topicName))
        {
            return Task.FromResult(false);
        }
        return Producer
            .ProduceAsync(topicName, new Message<string, MovementSaveRequest> { Key = key, Value = message })
            .ContinueWith(t => t.IsCompletedSuccessfully && t.Result.Status == PersistenceStatus.Persisted);
    }

    public void Dispose()
    {
        Producer?.Dispose();
        SchemRegistry?.Dispose();
    }
}