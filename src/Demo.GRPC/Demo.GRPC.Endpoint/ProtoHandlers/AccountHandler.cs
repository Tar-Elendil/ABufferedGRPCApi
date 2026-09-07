using Demo.GRPC.Endpoint.Services;
using Grpc.Core;

namespace Demo.GRPC.Endpoint.ProtoHandlers;

public class AccountHandler(IReadMovements movementsReader) : Account.AccountBase
{
    public override Task<BalanceResponse> Balance(BalanceRequest request, ServerCallContext context)
    {
        if (string.IsNullOrWhiteSpace(request.AccountId))
            return Task.FromResult(new BalanceResponse { Balance = double.NaN });

        return movementsReader.CheckBalance(request.AccountId)
            .ContinueWith(t => new BalanceResponse { Balance = t.Result });
    }

    public override Task<StatementExport> Statement(StatementExportRequest request, ServerCallContext context)
    {
        return Task.FromResult(new StatementExport
        {

        });
    }
}