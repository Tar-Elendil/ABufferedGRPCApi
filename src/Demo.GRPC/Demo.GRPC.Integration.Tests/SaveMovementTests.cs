using Demo.GRPC.Endpoint;
using Demo.GRPC.Integration.Tests.TestFixtures;

namespace Demo.GRPC.Integration.Tests;

public class SaveMovementTests(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper) : IntegrationTestBase(fixture, outputHelper)
{
    [Fact]
    public async Task Returns_Success_On_Movement_Save()
    {
        // Arrange
        var client = new Movements.MovementsClient(Channel);
        var movementRequest = new MovementSaveRequest { AccountId = "Account1", ExternalRef = "REF_123" };

        // Act

        var response = await client.AddAsync(movementRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.True(response.Success);
    }
}