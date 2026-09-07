using Demo.GRPC.Endpoint;
using Demo.GRPC.Integration.Tests.TestFixtures;

namespace Demo.GRPC.Integration.Tests;

public class AccountTests(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper) : IntegrationTestBase(fixture, outputHelper)
{
    [Fact]
    public async Task Returns_Success_On_Movement_Save()
    {
        // Arrange
        var client = new Account.AccountClient(Channel);
        var balanceRequest = new BalanceRequest
        {
            AccountId = "Account1",
        };

        // Act
        var response = await client.BalanceAsync(balanceRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.True(response.Balance > 0);
    }
}