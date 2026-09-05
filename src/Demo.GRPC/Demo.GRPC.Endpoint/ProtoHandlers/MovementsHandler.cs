using Demo.GRPC.Endpoint.Logging;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Demo.GRPC.Endpoint.ProtoHandlers;

public class MovementsHandler(ILogger<MovementsHandler> logger) : Movements.MovementsBase
{
    public override Task<MovementSaveReply> Add(MovementSaveRequest request, ServerCallContext context)
    {
        ProtoHandlerLogger.LogMovementRequest(logger, request);
        
        return Task.FromResult(new MovementSaveReply { Success = true });
    }
}