using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Demo.GRPC.Endpoint.ProtoHandlers;
using Demo.GRPC.Endpoint.Services;

namespace Demo.GRPC.Endpoint;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(o => o.EnableDetailedErrors = true);
        services.AddSingleton(new ProducerConfig
        {
            // Core Connection
            BootstrapServers = "localhost:29092",
            ClientId = "demo-grpc-endpoint",

            // Reliability & Durability
            Acks = Acks.All,
            MessageTimeoutMs = 30000,
            MessageMaxBytes = 1048576, // 1MB

            // Throughput & Optimization
            LingerMs = 20,
            BatchSize = 65536, // 64KB
            CompressionType = CompressionType.Snappy
        });
        services.AddSingleton(new SchemaRegistryConfig
        {
            Url = "http://localhost:8081"
        });
        services.AddSingleton<IPublishMessagesAsync<MovementSaveRequest>, MovementsPublisher>();
        services.AddSingleton<IValidateMovementRequests, MovementRequestValidator>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<MovementsHandler>();
        });
    }
}