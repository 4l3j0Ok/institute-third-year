using Cajero.Datos;

namespace Cajero.Logica;

public class Extraccion : Operacion
{
    public Extraccion(CentralDB central, Tarjeta tarjeta, Cuenta cuenta, decimal monto)
        : base(central, tarjeta, cuenta, monto)
    {
    }

    public override string Tipo
    {
        get => "Extracción";
    }

    public override bool Ejecutar()
    {
        var respuesta = _central.AutorizarDebito(_tarjeta.Numero, _monto);
        if (!respuesta.Autorizado)
        {
            _mensaje = respuesta.Motivo;
            return false;
        }
        _numeroComprobante = respuesta.NumeroTransaccion;
        _cuenta.ActualizarSaldo(respuesta.SaldoRestante, respuesta.SaldoRestante);
        _mensaje =
            $"Operación exitosa\nEfectivo dispensado: {Formato.Dinero(_monto)}\n" +
            $"Saldo restante: {Formato.Dinero(respuesta.SaldoRestante)}";
        return true;
    }
}
