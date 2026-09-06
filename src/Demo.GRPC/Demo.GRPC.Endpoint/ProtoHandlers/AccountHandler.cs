using Grpc.Core;

namespace Demo.GRPC.Endpoint.ProtoHandlers;

public class AccountHandler : Account.AccountBase
{
    public override Task<BalanceResponse> Balance(BalanceRequest request, ServerCallContext context)
    {
        return base.Balance(request, context);
    }

    public override Task<StatementExport> Statement(StatementExportRequest request, ServerCallContext context)
    {
        return base.Statement(request, context);
    }
}