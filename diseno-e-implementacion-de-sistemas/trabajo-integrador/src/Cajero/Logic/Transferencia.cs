using Cajero.Data;

namespace Cajero.Logic;

public class Transferencia : Operacion
{
    private string _cbuDestino;

    public Transferencia(CentralDB central, Tarjeta tarjeta, Cuenta cuenta, string cbuDestino, decimal monto)
        : base(central, tarjeta, cuenta, monto)
    {
        _cbuDestino = cbuDestino;
    }

    public string CbuDestino
    {
        get => _cbuDestino;
        set => _cbuDestino = value;
    }

    public override string TipoOperacion
    {
        get => "Transferencia";
    }

    public override bool Ejecutar()
    {
        var respuesta = _central.EjecutarTransferencia(_tarjeta.Numero, _cbuDestino, _monto);
        if (!respuesta.Ejecutada)
        {
            _mensaje = respuesta.Motivo;
            return false;
        }
        _numeroReferencia = respuesta.NumeroComprobante;
        _cuenta.ActualizarSaldo(_cuenta.SaldoDisponible - _monto, _cuenta.SaldoTotal - _monto);
        _mensaje =
            $"Transferencia realizada\nDestino: {_cbuDestino}\n" +
            $"Monto: {FormateadorMonedas.Formatear(_monto)}\nComprobante: {_numeroReferencia}";
        return true;
    }
}
