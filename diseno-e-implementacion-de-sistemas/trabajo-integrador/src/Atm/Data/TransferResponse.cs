namespace Atm.Data;

public record TransferResponse(bool Executed, string ReceiptNumber, string Reason = "");
