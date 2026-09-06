using Google.Protobuf;

namespace Demo.GRPC.Endpoint.Services;

public interface IPublishMessages<T> where T : IMessage
{
    bool Publish(T message, string key);
}

public interface IPublishMessagesAsync<T> where T : IMessage
{
    Task<bool> Publish(T message, string key);
}