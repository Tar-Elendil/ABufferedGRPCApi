using Google.Protobuf;

namespace Demo.StorageEngine.Handlers;

public interface IProcessMessages<T> where T : IMessage<T>
{
    void HandleMessage(T message);
}

public interface IProcessMessagesAsync<T> where T : IMessage<T>
{
    Task HandleMessage(T message);
}