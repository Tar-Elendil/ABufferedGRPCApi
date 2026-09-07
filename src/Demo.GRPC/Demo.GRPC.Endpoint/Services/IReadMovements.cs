namespace Demo.GRPC.Endpoint.Services;

public interface IReadMovements
{
    Task<double> CheckBalance(string accountId);
}