using Demo.GRPC.Endpoint.Models;

namespace Demo.GRPC.Endpoint.Services;

public interface IReadMovements
{
    Task<double> CheckBalance(string accountId);
    IAsyncEnumerable<Movement> ExportAccount(string accountId);
}