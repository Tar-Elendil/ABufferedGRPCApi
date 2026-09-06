using Demo.GRPC.Endpoint.ProtoHandlers;
using Demo.GRPC.Endpoint.Services;

namespace Demo.GRPC.Endpoint;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(o => o.EnableDetailedErrors = true);
        services.AddSingleton<IPublishMessagesAsync<MovementSaveRequest>, MovementsPublisher>();
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