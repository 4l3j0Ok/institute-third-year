using System;
using Cajero.Datos;

namespace Cajero.Logica;

public class Consulta : Operacion
{
    public Consulta(CentralDB central, Tarjeta tarjeta, Cuenta cuenta)
        : base(central, tarjeta, cuenta, 0m)
    {
    }

    public override string Tipo
    {
        get => "Consulta de saldo";
    }

    public override bool Ejecutar()
    {
        var respuesta = _central.ConsultarSaldo(_tarjeta.Numero);
        _cuenta.ActualizarSaldo(respuesta.Disponible, respuesta.Total);
        _mensaje =
            $"Saldo disponible: {Formato.Dinero(respuesta.Disponible)}\n" +
            $"Saldo total: {Formato.Dinero(respuesta.Total)}\n" +
            $"{DateTime.Now:dd/MM/yyyy HH:mm}";
        return true;
    }
}
