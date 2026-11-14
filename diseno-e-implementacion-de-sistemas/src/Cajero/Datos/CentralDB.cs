using System;
using System.Collections.Generic;
using System.Linq;

namespace Cajero.Datos;

/// <summary>Simula la comunicación con la Red Central (datos en memoria).</summary>
public class CentralDB
{
    public const decimal LimiteDiario = 100_000m;

    private static readonly Random s_azar = new();

    // SHA-256 de cada PIN: Red Central nunca recibe ni guarda el PIN en claro
    private readonly Dictionary<string, string> _pinesEncriptados = new()
    {
        ["1234567890123456"] = "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4",
        ["5555666677778888"] = "9AF15B336E6A9619928537DF30B2E6A2376569FCF9D7E773ECCEDE65606529A0",
        ["1111222233334444"] = "9AF15B336E6A9619928537DF30B2E6A2376569FCF9D7E773ECCEDE65606529A0",
    };

    private decimal _saldo = 150_000m;
    private decimal _extraidoHoy;

    public bool ValidarPin(string numeroTarjeta, string pinEncriptado)
    {
        return _pinesEncriptados.TryGetValue(numeroTarjeta, out var esperado) && esperado == pinEncriptado;
    }

    public RespuestaSaldo ConsultarSaldo(string numeroTarjeta)
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
        return $"{prefijo}{s_azar.Next(10_000_000, 100_000_000)}";
    }
}
