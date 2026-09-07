namespace Demo.GRPC.Endpoint.Services;

public interface IValidateMovementRequests
{
    bool Validate(MovementSaveRequest request);
}

public class MovementRequestValidator : IValidateMovementRequests
{
    public bool Validate(MovementSaveRequest request)
    {
        if (request == null)
            return false;

        return !(string.IsNullOrWhiteSpace(request.AccountId) || 
            string.IsNullOrWhiteSpace(request.ExternalRef));
    }
}