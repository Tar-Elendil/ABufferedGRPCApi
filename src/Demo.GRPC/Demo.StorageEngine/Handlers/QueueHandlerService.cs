using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Demo.Storage.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.StorageEngine.Handlers;

public class QueueHandlerService(SaveMovementHandler handler, ConsumerConfig config, ILogger<QueueHandlerService> logger) : IHostedService, IDisposable
{
    // Hard coded topics to subscribe to.
    // Use a configuration setup service extension in future
    private readonly List<string> Topics = [
        ""
    ];

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting kafka subscriber");

        using var consumer = new ConsumerBuilder<string, MovementSaveRequest>(config)
            // Note: All handlers are called on the main .Consume thread.
            .SetErrorHandler((_, e) => logger.LogError("Error: {Reason}", e.Reason))
            .SetValueDeserializer(new ProtobufDeserializer<MovementSaveRequest>().AsSyncOverAsync())
            .Build();

        consumer.Subscribe(Topics);

        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(cancellationToken);

            if (consumeResult?.Message?.Value == null)
            {
                continue;
            }
            var request = consumeResult.Message.Value;
            ProtoHandlerLogger.LogMovementRequest(logger, request);
            await handler.HandleMessage(request);
        }
        logger.LogInformation("Stopping kafka subscriber");
        consumer.Close();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        
    }

    public void Dispose()
    {
    }
}