using Demo.GRPC.Endpoint.Models;
using Demo.GRPC.Endpoint.Services;
using Grpc.Core;
using System.Globalization;
using System.Text;
using static Confluent.Kafka.ConfigPropertyNames;
using static System.Net.Mime.MediaTypeNames;

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


    public override async Task Statement(StatementExportRequest request, IServerStreamWriter<StatementExport> responseStream, ServerCallContext context)
    {
        // 64 KB chunk size is a balanced default for gRPC streams
        const int bufferSize = 64 * 1024;
        var movements = movementsReader.ExportAccount(request.AccountId);

        using var ms = new MemoryStream();

        // Header Row
        await ms.WriteAsync(Encoding.UTF8.GetBytes("AccountId,ExternalRef,Currency,Amount,OccurredAt,Narration\n"));

        await foreach (var item in movements)
        {
            // escape values if they contain commas or quotes
            var line = $"{EscapeCsvField(item.AccountId)},{EscapeCsvField(item.ExternalRef)},{item.Currency},{EscapeNumeric(item.Amount)},{item.OccurredAt},{EscapeCsvField(item.Narration)}\n";
            var strLine = Encoding.UTF8.GetBytes(line);
            if (ms.Length + strLine.Length > bufferSize)
            {
                // Convert buffer chunk to Google.Protobuf.ByteString
                var response = new StatementExport
                {
                    Chunk = Google.Protobuf.ByteString.CopyFrom(ms.GetBuffer())
                };

                // Stream the chunk to the client
                await responseStream.WriteAsync(response);

                ms.SetLength(0);
            }

            await ms.WriteAsync(strLine);
        }
        if (ms.Length > 0)
        {
            // Send final chunk
            var response = new StatementExport
            {
                Chunk = await Google.Protobuf.ByteString.FromStreamAsync(ms)
            };

            await responseStream.WriteAsync(response);
        }
    }
    private static string EscapeNumeric(double value)
    {
        return value.ToString(NumberFormatInfo.InvariantInfo);
    }
    private static string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field)) return string.Empty;
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }
}