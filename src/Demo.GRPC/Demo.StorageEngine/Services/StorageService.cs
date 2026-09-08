using Demo.StorageEngine.Models;
using Microsoft.Extensions.Logging;

namespace Demo.StorageEngine.Services;

public class StorageService(Db db, ILogger<StorageService> logger) : IDisposable, IStoreObjects
{
    private bool disposedValue;

    public async Task Save<T>(T obj) where T : class, IAmADatabaseObject
    {
        try
        {
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