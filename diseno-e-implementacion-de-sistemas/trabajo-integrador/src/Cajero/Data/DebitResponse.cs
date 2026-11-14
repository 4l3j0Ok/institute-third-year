namespace Cajero.Data;

public record DebitResponse(bool Authorized, string TransactionNumber, decimal RemainingBalance, string Reason = "");
