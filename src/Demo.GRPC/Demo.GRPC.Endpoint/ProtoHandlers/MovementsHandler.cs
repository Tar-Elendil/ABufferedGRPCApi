using Demo.GRPC.Endpoint.Logging;
using Demo.GRPC.Endpoint.Services;
using Grpc.Core;

namespace Demo.GRPC.Endpoint.ProtoHandlers;

public class MovementsHandler(IPublishMessagesAsync<MovementSaveRequest> publisher, IValidateMovementRequests validator, ILogger<MovementsHandler> logger) 
    : Movements.MovementsBase
{
    public override Task<MovementSaveReply> Add(MovementSaveRequest request, ServerCallContext context)
    {
        ProtoHandlerLogger.LogMovementRequest(logger, request);

        if (!validator.Validate(request))
            return Task.FromResult(new MovementSaveReply { Success = false });

        return publisher.Publish(request, "")
            .ContinueWith(t =>
            {
                var success = t.IsCompletedSuccessfully && t.Result;
                return new MovementSaveReply { Success = success };
            });
    }
}