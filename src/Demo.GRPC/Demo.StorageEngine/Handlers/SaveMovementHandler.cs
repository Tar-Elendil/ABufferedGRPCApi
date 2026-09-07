using Demo.StorageEngine.Models;
using Demo.StorageEngine.Services;

namespace Demo.StorageEngine.Handlers;

public class SaveMovementHandler(IStoreObjects storageService) : IProcessMessagesAsync<MovementSaveRequest>
{
    public Task HandleMessage(MovementSaveRequest message)
    {
        var movement = new Movement
        {
            AccountId = message.AccountId,
            ExternalRef = message.ExternalRef,
            Currency = message.Currency,
            Amount = message.Amount,
            OccurredAt = message.OccurredAt.ToDateTime(),
            Narration = message.Narration,
        };
        return storageService.Save(movement);
    }
}