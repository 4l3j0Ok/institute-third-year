using System;
using Cajero.Data;

namespace Cajero.Logic;

public class Consulta : Operacion
{
    public Consulta(CentralDB central, Tarjeta tarjeta, Cuenta cuenta)
        : base(central, tarjeta, cuenta, 0m)
    {
    }

    public override string TipoOperacion
    {
        get => "Consulta de saldo";
    }

    public override bool Ejecutar()
    {
        var respuesta = _central.ObtenerSaldo(_tarjeta.Numero);
        _cuenta.ActualizarSaldo(respuesta.Disponible, respuesta.Total);
        _mensaje =
            $"Saldo disponible: {FormateadorMonedas.Formatear(respuesta.Disponible)}\n" +
            $"Saldo total: {FormateadorMonedas.Formatear(respuesta.Total)}\n" +
            $"{DateTime.Now:dd/MM/yyyy HH:mm}";
        return true;
    }
}
