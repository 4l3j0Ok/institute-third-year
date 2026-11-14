namespace Cajero.Datos;

public record RespuestaTransferencia(bool Ejecutada, string NumeroComprobante, string Motivo = "");
