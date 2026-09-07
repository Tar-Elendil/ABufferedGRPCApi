using Demo.GRPC.Endpoint.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.GRPC.Endpoint.Services;

public class MovementsReader(Db db, ILogger<MovementsReader> logger) : IDisposable, IReadMovements
{
    private bool disposedValue;

    public Task<double> CheckBalance(string accountId)
    {
        return db.Movements
            .Where(m => string.Equals(m.AccountId, accountId))
            .SumAsync(m => m.Amount);
    }

    public IAsyncEnumerable<Movement> ExportAccount(string accountId, DateTime start, DateTime end)
    {
        return db.Movements
            .Where(m => string.Equals(m.AccountId, accountId))
            .Where(m => m.OccurredAt >= start && m.OccurredAt <= end)
            .AsNoTracking()
            .AsAsyncEnumerable();
    }

    protected void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                db?.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}