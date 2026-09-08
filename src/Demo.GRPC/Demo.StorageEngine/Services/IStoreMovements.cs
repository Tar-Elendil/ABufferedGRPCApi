using Demo.StorageEngine.Models;

namespace Demo.StorageEngine.Services;

public interface IStoreMovements
{
    Task<bool> VerifyConnection();
    Task Save(Movement obj);
}