using Demo.GRPC.Endpoint;
using Demo.GRPC.Integration.Tests.TestFixtures;
using Google.Protobuf.WellKnownTypes;

namespace Demo.GRPC.Integration.Tests;

public class SaveMovementTests(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper) : IntegrationTestBase(fixture, outputHelper)
{
    [Fact]
    public async Task Returns_Success_On_Movement_Save()
    {
        // Arrange
        var client = new Movements.MovementsClient(Channel);
        var movementRequest = new MovementSaveRequest 
        { 
            AccountId = "Account1", ExternalRef = Guid.NewGuid().ToString(),
            Amount = Random.Shared.Next(1, 100), Currency = "ZAR",
            OccurredAt = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(-Random.Shared.Next(1, 8))),
            Narration = ""
        };

        // Act
        var response = await client.AddAsync(movementRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.True(response.Success);
    }
}