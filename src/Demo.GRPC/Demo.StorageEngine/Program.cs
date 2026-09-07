using Demo.StorageEngine.Extensions;
using Microsoft.Extensions.Hosting;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.AddStorageService();

IHost host = builder.Build();
host.Run();