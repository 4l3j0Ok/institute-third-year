using System;
using Cajero.Datos;

namespace Cajero.Logica;

public class Tarjeta
{
    public const int MaximoIntentos = 3;

    private readonly string _numero;
    private readonly DateTime _vencimiento;
    private EstadoTarjeta _estado;
    private int _intentosPin;

    public Tarjeta(string numero, DateTime vencimiento)
    {
        _numero = numero;
        _vencimiento = vencimiento;
        _estado = EstadoTarjeta.Valida;
    }

    public string Numero
    {
        get => _numero;
    }

    public DateTime Vencimiento
    {
        get => _vencimiento;
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

    public bool EstaVigente()
    {
        return _estado == EstadoTarjeta.Valida && _vencimiento.Date >= DateTime.Today;
    }

    public int RegistrarIntentoFallido()
    {
        _intentosPin++;
        if (_intentosPin >= MaximoIntentos)
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
