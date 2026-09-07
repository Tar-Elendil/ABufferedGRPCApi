using Demo.StorageEngine.Models;

namespace Demo.StorageEngine.Services;

public interface IStoreObjects
{
    Task<bool> VerifyConnection();
    Task Save<T>(T obj) where T : class, IAmADatabaseObject;
}