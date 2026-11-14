namespace Cajero.Data;

public record TransferResponse(bool Executed, string ReceiptNumber, string Reason = "");
