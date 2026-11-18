using System;
using System.Collections.Generic;

namespace Cajero.Data;

/// <summary>Simula la comunicación con la Sucursal; datos en memoria.</summary>
public class SucursalDB
{
    private readonly Dictionary<string, EstadoTarjeta> _tarjetas = new()
    {
        ["1234567890123456"] = EstadoTarjeta.Valida,
        ["1111222233334444"] = EstadoTarjeta.Bloqueada,
        ["5555666677778888"] = EstadoTarjeta.Vencida,
    };

    public EstadoTarjeta ValidarTarjeta(string numero, DateTime fechaVencimiento)
    {
        if (!_tarjetas.TryGetValue(numero, out var estado))
        {
            return EstadoTarjeta.Bloqueada;
        }
        if (fechaVencimiento.Date < DateTime.Today)
        {
            return EstadoTarjeta.Vencida;
        }
        return estado;
    }

    public void BloquearTarjeta(string numero)
    {
        _tarjetas[numero] = EstadoTarjeta.Bloqueada;
    }
}
