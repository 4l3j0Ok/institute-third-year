using System;
using Cajero.Datos;

namespace Cajero.Logica;

public abstract class Operacion
{
    protected readonly CentralDB _central;
    protected readonly Tarjeta _tarjeta;
    protected readonly Cuenta _cuenta;
    protected decimal _monto;
    protected readonly DateTime _fecha;
    protected string _numeroComprobante;
    protected string _mensaje;

    protected Operacion(CentralDB central, Tarjeta tarjeta, Cuenta cuenta, decimal monto)
    {
        _central = central;
        _tarjeta = tarjeta;
        _cuenta = cuenta;
        _monto = monto;
        _fecha = DateTime.Now;
        _numeroComprobante = "";
        _mensaje = "";
    }

    public decimal Monto
    {
        get => _monto;
        set => _monto = value;
    }

    public DateTime Fecha
    {
        get => _fecha;
    }

    public string NumeroComprobante
    {
        get => _numeroComprobante;
    }

    public string Mensaje
    {
        get => _mensaje;
    }

    public abstract string Tipo { get; }

    public abstract bool Ejecutar();

    public override string ToString()
    {
        return $"{_fecha:HH:mm} {Tipo} {Formato.Dinero(_monto)} {_numeroComprobante}".Trim();
    }
}
