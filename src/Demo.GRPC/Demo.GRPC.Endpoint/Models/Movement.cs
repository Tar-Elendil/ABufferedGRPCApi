using Microsoft.EntityFrameworkCore;

namespace Demo.GRPC.Endpoint.Models;

[PrimaryKey(nameof(AccountId), nameof(ExternalRef))]
public class Movement : IAmADatabaseObject
{
    public string AccountId { get; set; } = string.Empty;
    public string ExternalRef { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public double Amount { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Narration { get; set; } = string.Empty;
}