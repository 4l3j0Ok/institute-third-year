using System;
using Cajero.Data;

namespace Cajero.Logic;

public abstract class Operacion
{
    protected readonly CentralDB _central;
    protected readonly Tarjeta _tarjeta;
    protected readonly Cuenta _cuenta;
    protected decimal _monto;
    protected readonly DateTime _fechaHora;
    protected string _numeroReferencia;
    protected string _mensaje;

    protected Operacion(CentralDB central, Tarjeta tarjeta, Cuenta cuenta, decimal monto)
    {
        _central = central;
        _tarjeta = tarjeta;
        _cuenta = cuenta;
        _monto = monto;
        _fechaHora = DateTime.Now;
        _numeroReferencia = "";
        _mensaje = "";
    }

    public decimal Monto
    {
        get => _monto;
        set => _monto = value;
    }

    public DateTime FechaHora
    {
        get => _fechaHora;
    }

    /// <summary>Número de transacción (TX) o de comprobante (TR) asignado por la Red Central.</summary>
    public string NumeroReferencia
    {
        get => _numeroReferencia;
    }

    public string Mensaje
    {
        get => _mensaje;
    }

    public abstract string TipoOperacion { get; }

    public abstract bool Ejecutar();

    public override string ToString()
    {
        return $"{_fechaHora:HH:mm} {TipoOperacion} {FormateadorMonedas.Formatear(_monto)} {_numeroReferencia}".Trim();
    }
}
