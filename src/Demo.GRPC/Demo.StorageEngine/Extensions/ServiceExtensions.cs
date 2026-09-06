using Demo.StorageEngine.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.StorageEngine.Extensions;

public static class ServiceExtensions
{
    public static IHostBuilder AddStorageService(this IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        });
        return builder;
    }
}