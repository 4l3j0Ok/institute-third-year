namespace Cajero.Datos;

public record RespuestaDebito(bool Autorizado, string NumeroTransaccion, decimal SaldoRestante, string Motivo = "");
