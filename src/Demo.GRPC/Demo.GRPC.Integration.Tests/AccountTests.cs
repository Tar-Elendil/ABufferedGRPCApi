using Demo.GRPC.Endpoint;
using Demo.GRPC.Integration.Tests.TestFixtures;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Demo.GRPC.Integration.Tests;

public class AccountTests(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper) : IntegrationTestBase(fixture, outputHelper)
{
    [Fact]
    public async Task Returns_NonZero_Balance_When_Entries_Already_Exist()
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

    [Fact]
    public async Task Can_DownloaExportedStatement_When_Entries_Exist()
    {
        // Arrange
        var client = new Account.AccountClient(Channel);
        var statementRequest = new StatementExportRequest
        {
            AccountId = "Account1",
            Index = 0,
            Start = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(-12)),
            End = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(+12)),
        };
        var filePath = "export.csv";


        // Act
        using var response = client.Statement(statementRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        try
        {
            using var fileStream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write, FileShare.None);

            // Read the stream sequentially until completion
            await foreach (var page in response.ResponseStream.ReadAllAsync(TestContext.Current.CancellationToken))
            {
                await fileStream.WriteAsync(page.Chunk.Memory, TestContext.Current.CancellationToken);
            }

            Console.WriteLine("Download completed successfully!");
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
        {
            Console.WriteLine("Error: The requested file does not exist on the server.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }
}