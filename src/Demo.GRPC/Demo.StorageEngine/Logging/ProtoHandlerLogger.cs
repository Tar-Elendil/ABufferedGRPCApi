using Microsoft.Extensions.Logging;

namespace Demo.Storage.Logging;

public static partial class ProtoHandlerLogger
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "Received movement request: {Movement}")]
    public static partial void LogMovementRequest(
        ILogger logger, MovementSaveRequest movement);
}