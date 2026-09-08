using Demo.StorageEngine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.StorageEngine.Services;

public class MovementsStorageService(Db db, ILogger<MovementsStorageService> logger) : IDisposable, IStoreMovements
{
    private bool disposedValue;

    public async Task Save(Movement obj)
    {
        try
        {
            if (await db.Movements.AsNoTracking()
                .AnyAsync(x => string.Equals(x.AccountId, obj.AccountId) 
                && string.Equals(x.ExternalRef, obj.ExternalRef)))
            {
                return;
            }
            db.Add(obj);
            await db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unable to write to database");
        }
    }

    public Task<bool> VerifyConnection()
    {
        return db.Database.CanConnectAsync();
    }

    protected virtual void Dispose(bool disposing)
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