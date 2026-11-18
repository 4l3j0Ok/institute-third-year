using Cajero.Data;

namespace Cajero.Logic;

public class Extraccion : Operacion
{
    public Extraccion(CentralDB central, Tarjeta tarjeta, Cuenta cuenta, decimal monto)
        : base(central, tarjeta, cuenta, monto)
    {
    }

    public override string TipoOperacion
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
        _numeroReferencia = respuesta.NumeroTransaccion;
        _cuenta.ActualizarSaldo(respuesta.SaldoRestante, respuesta.SaldoRestante);
        _mensaje =
            $"Operación exitosa\nEfectivo dispensado: {FormateadorMonedas.Formatear(_monto)}\n" +
            $"Saldo restante: {FormateadorMonedas.Formatear(respuesta.SaldoRestante)}";
        return true;
    }
}
