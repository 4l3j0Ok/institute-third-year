using System;
using Cajero.Data;

namespace Cajero.Logic;

public class Tarjeta
{
    public const int IntentosMaximosPin = 3;

    private readonly string _numero;
    private readonly DateTime _fechaVencimiento;
    private EstadoTarjeta _estado;
    private int _intentosPin;

    public Tarjeta(string numero, DateTime fechaVencimiento)
    {
        _numero = numero;
        _fechaVencimiento = fechaVencimiento;
        _estado = EstadoTarjeta.Valida;
    }

    public string Numero
    {
        get => _numero;
    }

    public DateTime FechaVencimiento
    {
        get => _fechaVencimiento;
    }

    public EstadoTarjeta Estado
    {
        get => _estado;
        set => _estado = value;
    }

    public int IntentosPin
    {
        get => _intentosPin;
    }

    public bool EsValida()
    {
        return _estado == EstadoTarjeta.Valida && _fechaVencimiento.Date >= DateTime.Today;
    }

    public int RegistrarIntentoFallido()
    {
        _intentosPin++;
        if (_intentosPin >= IntentosMaximosPin)
        {
            Bloquear();
        }
        return _intentosPin;
    }

    public void Bloquear()
    {
        _estado = EstadoTarjeta.Bloqueada;
    }
}
