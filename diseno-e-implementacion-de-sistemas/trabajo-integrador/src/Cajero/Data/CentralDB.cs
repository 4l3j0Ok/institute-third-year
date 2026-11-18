using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Data;

/// <summary>Simula la comunicación con la Red Central; datos en memoria.</summary>
public class CentralDB
{
    public const decimal LimiteDiario = 100_000m;

    private static readonly Random s_random = new();

    private readonly Dictionary<string, string> _pines = new()
    {
        ["1234567890123456"] = "1234",
        ["5555666677778888"] = "0000",
        ["1111222233334444"] = "0000",
    };

    private decimal _saldo = 150_000m;
    private decimal _extraidoHoy;

    public bool ValidarPin(string numeroTarjeta, string pin)
    {
        return _pines.TryGetValue(numeroTarjeta, out var esperado) && esperado == pin;
    }

    public RespuestaSaldo ObtenerSaldo(string numeroTarjeta)
    {
        return new RespuestaSaldo(_saldo, _saldo);
    }

    public RespuestaDebito AutorizarDebito(string numeroTarjeta, decimal monto)
    {
        if (monto <= 0)
        {
            return new RespuestaDebito(false, "", _saldo, "Monto inválido");
        }
        if (monto > _saldo)
        {
            return new RespuestaDebito(false, "", _saldo, "Saldo insuficiente");
        }
        if (_extraidoHoy + monto > LimiteDiario)
        {
            return new RespuestaDebito(false, "", _saldo, "Supera el límite diario");
        }
        _saldo -= monto;
        _extraidoHoy += monto;
        return new RespuestaDebito(true, GenerarNumero("TX"), _saldo);
    }

    public RespuestaTransferencia EjecutarTransferencia(string numeroTarjeta, string cbuDestino, decimal monto)
    {
        if (cbuDestino.Length != 22 || !cbuDestino.All(char.IsAsciiDigit))
        {
            return new RespuestaTransferencia(false, "", "CBU inválido");
        }
        if (monto <= 0)
        {
            return new RespuestaTransferencia(false, "", "Monto inválido");
        }
        if (monto > _saldo)
        {
            return new RespuestaTransferencia(false, "", "Saldo insuficiente");
        }
        _saldo -= monto;
        return new RespuestaTransferencia(true, GenerarNumero("TR"));
    }

    private static string GenerarNumero(string prefijo)
    {
        return $"{prefijo}{s_random.Next(10_000_000, 100_000_000)}";
    }
}
