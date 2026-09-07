using Confluent.Kafka;
using Demo.StorageEngine.Handlers;
using Demo.StorageEngine.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.StorageEngine.Extensions;

public static class ServiceExtensions
{
    public static IHostBuilder AddStorageService(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<Db>(options =>
            {
                options
                .UseNpgsql("Host=localhost;Port=5432;Username=admin;Password=example;Database=demo")
                .UseSnakeCaseNamingConvention();
            });
            services.AddSingleton<ConsumerConfig>(new ConsumerConfig
            {
                // 1. Core Connection
                BootstrapServers = "localhost:29092",

                // 2. Identity and Grouping
                GroupId = "database-writers",
                ClientId = "storage-service-1",

                // 3. Offset Management
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = true
            });
            services.AddSingleton<IStoreObjects, StorageService>();
            services.AddSingleton<SaveMovementHandler>();
            services.AddHostedService<QueueHandlerService>();
        });
        return builder;
    }
}