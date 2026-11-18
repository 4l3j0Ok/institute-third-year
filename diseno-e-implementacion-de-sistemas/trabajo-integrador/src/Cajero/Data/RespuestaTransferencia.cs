namespace Cajero.Data;

public record RespuestaTransferencia(bool Ejecutada, string NumeroComprobante, string Motivo = "");
