using System;
using System.Collections.Generic;
using System.Linq;
using Cajero.Data;

namespace Cajero.Logic;

public class Sesion
{
    private readonly SucursalDB _sucursal;
    private readonly CentralDB _central;
    private readonly List<Operacion> _operaciones;
    private readonly Cuenta _cuenta;
    private readonly Cliente _cliente;
    private readonly DateTime _horaInicio;
    private Tarjeta? _tarjeta;
    private EstadoSesion _estado;
    private string _mensaje;

    public Sesion()
    {
        _sucursal = new SucursalDB();
        _central = new CentralDB();
        _operaciones = [];
        _cuenta = new Cuenta("0170099920000012345678", 150_000m, 150_000m);
        _cliente = new Cliente("Cliente Demo", "30111222");
        _horaInicio = DateTime.Now;
        _estado = EstadoSesion.Iniciada;
        _mensaje = "";
    }

    public EstadoSesion Estado
    {
        get => _estado;
    }

    public Tarjeta? Tarjeta
    {
        get => _tarjeta;
    }

    public Cuenta Cuenta
    {
        get => _cuenta;
    }

    public Cliente Cliente
    {
        get => _cliente;
    }

    public DateTime HoraInicio
    {
        get => _horaInicio;
    }

    public string Mensaje
    {
        get => _mensaje;
    }

    public IReadOnlyList<Operacion> Operaciones
    {
        get => _operaciones.AsReadOnly();
    }

    private Tarjeta TarjetaActual
    {
        get => _tarjeta ?? throw new InvalidOperationException("No se insertó ninguna tarjeta en la sesión.");
    }

    /// <summary>Iniciada → Autenticando → Validando (tarjeta válida) o Cerrando (bloqueada o vencida).</summary>
    public bool InsertarTarjeta(string numero, DateTime fechaVencimiento)
    {
        _estado = EstadoSesion.Autenticando;
        _tarjeta = new Tarjeta(numero, fechaVencimiento);
        var estadoTarjeta = _sucursal.ValidarTarjeta(numero, fechaVencimiento);
        _tarjeta.Estado = estadoTarjeta;
        if (estadoTarjeta != EstadoTarjeta.Valida)
        {
            _mensaje = estadoTarjeta == EstadoTarjeta.Vencida ? "Tarjeta vencida" : "Tarjeta bloqueada";
            _estado = EstadoSesion.Cerrando;
            return false;
        }
        _estado = EstadoSesion.Validando;
        return true;
    }

    /// <summary>Validando → Operando (PIN correcto), Validando (reintento) o Cerrando (3 intentos fallidos).</summary>
    public bool Autenticar(string pin)
    {
        if (_tarjeta is null)
        {
            return false;
        }
        if (_central.ValidarPin(_tarjeta.Numero, pin))
        {
            _estado = EstadoSesion.Operando;
            return true;
        }
        var intentos = _tarjeta.RegistrarIntentoFallido();
        if (intentos >= Tarjeta.IntentosMaximosPin)
        {
            _sucursal.BloquearTarjeta(_tarjeta.Numero);
            _mensaje = "PIN incorrecto 3 veces\nTarjeta bloqueada";
            _estado = EstadoSesion.Cerrando;
        }
        else
        {
            _mensaje = $"PIN incorrecto\nIntentos restantes: {Tarjeta.IntentosMaximosPin - intentos}";
        }
        return false;
    }

    /// <summary>Operando → Operando. Solo las operaciones exitosas quedan en el log de la sesión.</summary>
    public bool Ejecutar(Operacion operacion)
    {
        var exito = operacion.Ejecutar();
        _mensaje = operacion.Mensaje;
        if (exito)
        {
            _operaciones.Add(operacion);
        }
        return exito;
    }

    public Operacion NuevaConsulta()
    {
        return new Consulta(_central, TarjetaActual, _cuenta);
    }

    public Operacion NuevaExtraccion(decimal monto)
    {
        return new Extraccion(_central, TarjetaActual, _cuenta, monto);
    }

    public Operacion NuevaTransferencia(string cbuDestino, decimal monto)
    {
        return new Transferencia(_central, TarjetaActual, _cuenta, cbuDestino, monto);
    }

    /// <summary>Validando u Operando → Cerrando.</summary>
    public void Finalizar()
    {
        _estado = EstadoSesion.Cerrando;
    }

    public string GenerarComprobante()
    {
        List<string> lineas = ["*** BANCO ISFT 93 ***", "COMPROBANTE", ""];
        lineas.AddRange(_operaciones.Count > 0 ? _operaciones.Select(operacion => operacion.ToString()) : ["Sin operaciones"]);
        return string.Join("\n", lineas);
    }

    /// <summary>Cerrando → Cerrada: elimina de memoria los datos de autenticación.</summary>
    public void Cerrar()
    {
        _tarjeta = null;
        _operaciones.Clear();
        _estado = EstadoSesion.Cerrada;
    }
}
