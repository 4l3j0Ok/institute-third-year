using Cajero.Datos;
using Cajero.Logica;

namespace Cajero.Tests;

public class SesionTests
{
    private const string TarjetaValida = "1234567890123456";
    private const string TarjetaBloqueada = "1111222233334444";
    private const string TarjetaVencida = "5555666677778888";
    private static readonly DateTime s_futuro = new DateTime(2099, 12, 31);

    private static Sesion SesionAutenticada()
    {
        var sesion = new Sesion();
        Assert.That(sesion.IngresarTarjeta(TarjetaValida, s_futuro), Is.True);
        Assert.That(sesion.Autenticar("1234"), Is.True);
        return sesion;
    }

    [Test]
    public void TarjetaValida_PasaAValidando()
    {
        var sesion = new Sesion();

        Assert.That(sesion.IngresarTarjeta(TarjetaValida, s_futuro), Is.True);
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Validando));
    }

    [Test]
    public void TarjetaBloqueada_CierraLaSesion()
    {
        var sesion = new Sesion();

        Assert.That(sesion.IngresarTarjeta(TarjetaBloqueada, s_futuro), Is.False);
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrando));
        Assert.That(sesion.Mensaje, Is.EqualTo("Tarjeta bloqueada"));
    }

    [Test]
    public void TarjetaVencida_CierraLaSesion()
    {
        var sesion = new Sesion();

        Assert.That(sesion.IngresarTarjeta(TarjetaVencida, new DateTime(2020, 1, 31)), Is.False);
        Assert.That(sesion.Mensaje, Is.EqualTo("Tarjeta vencida"));
    }

    [Test]
    public void PinCorrecto_PasaAOperando()
    {
        var sesion = SesionAutenticada();

        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Operando));
    }

    [Test]
    public void PinIncorrecto_InformaIntentosRestantes()
    {
        var sesion = new Sesion();
        sesion.IngresarTarjeta(TarjetaValida, s_futuro);

        Assert.That(sesion.Autenticar("0000"), Is.False);
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Validando));
        Assert.That(sesion.Mensaje, Does.Contain("Intentos restantes: 2"));
    }

    [Test]
    public void TresPinesIncorrectos_BloqueanLaTarjeta()
    {
        var sesion = new Sesion();
        sesion.IngresarTarjeta(TarjetaValida, s_futuro);

        sesion.Autenticar("0000");
        sesion.Autenticar("0000");
        sesion.Autenticar("0000");

        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrando));
        Assert.That(sesion.Tarjeta!.Estado, Is.EqualTo(EstadoTarjeta.Bloqueada));
    }

    [Test]
    public void Extraccion_DescuentaElSaldo()
    {
        var sesion = SesionAutenticada();

        Assert.That(sesion.Ejecutar(sesion.NuevaExtraccion(10_000m)), Is.True);
        Assert.That(sesion.Cuenta.SaldoDisponible, Is.EqualTo(140_000m));
        Assert.That(sesion.Operaciones, Has.Count.EqualTo(1));
    }

    [Test]
    public void Extraccion_RechazaMontoMayorAlSaldo()
    {
        var sesion = SesionAutenticada();

        Assert.That(sesion.Ejecutar(sesion.NuevaExtraccion(200_000m)), Is.False);
        Assert.That(sesion.Mensaje, Is.EqualTo("Saldo insuficiente"));
        Assert.That(sesion.Operaciones, Is.Empty);
    }

    [Test]
    public void Extraccion_RechazaSuperarElLimiteDiario()
    {
        var sesion = SesionAutenticada();
        sesion.Ejecutar(sesion.NuevaExtraccion(90_000m));

        Assert.That(sesion.Ejecutar(sesion.NuevaExtraccion(20_000m)), Is.False);
        Assert.That(sesion.Mensaje, Is.EqualTo("Supera el límite diario"));
    }

    [Test]
    public void Transferencia_ConCbuInvalido_Falla()
    {
        var sesion = SesionAutenticada();

        Assert.That(sesion.Ejecutar(sesion.NuevaTransferencia("123", 1_000m)), Is.False);
        Assert.That(sesion.Mensaje, Is.EqualTo("CBU inválido"));
    }

    [Test]
    public void Transferencia_Valida_DescuentaElSaldoYGeneraComprobante()
    {
        var sesion = SesionAutenticada();
        var transferencia = sesion.NuevaTransferencia("0170099920000012345678", 2_500m);

        Assert.That(sesion.Ejecutar(transferencia), Is.True);
        Assert.That(sesion.Cuenta.SaldoDisponible, Is.EqualTo(147_500m));
        Assert.That(transferencia.NumeroComprobante, Does.StartWith("TR"));
    }

    [Test]
    public void Ticket_ListaLasOperacionesRealizadas()
    {
        var sesion = SesionAutenticada();
        sesion.Ejecutar(sesion.NuevaConsulta());
        sesion.Ejecutar(sesion.NuevaExtraccion(1_000m));

        var ticket = sesion.GenerarTicket();

        Assert.That(ticket, Does.Contain("Consulta de saldo"));
        Assert.That(ticket, Does.Contain("Extracción $1.000,00"));
    }

    [Test]
    public void Cerrar_LimpiaTarjetaYOperaciones()
    {
        var sesion = SesionAutenticada();
        sesion.Ejecutar(sesion.NuevaConsulta());

        sesion.Cerrar();

        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrada));
        Assert.That(sesion.Tarjeta, Is.Null);
        Assert.That(sesion.Operaciones, Is.Empty);
    }

    [Test]
    public void Autenticar_PermiteExactamenteTresIntentos()
    {
        var sesion = new Sesion();
        sesion.IngresarTarjeta(TarjetaValida, s_futuro);

        sesion.Autenticar("0000");
        sesion.Autenticar("0000");
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Validando));

        sesion.Autenticar("0000");
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrando));
        Assert.That(sesion.Tarjeta!.IntentosPin, Is.EqualTo(Tarjeta.MaximoIntentos));
    }

    [Test]
    public void TarjetaBloqueadaPorPin_SeRechazaEnLaSiguienteSesion()
    {
        var sesion = new Sesion();
        sesion.IngresarTarjeta(TarjetaValida, s_futuro);
        sesion.Autenticar("0000");
        sesion.Autenticar("0000");
        sesion.Autenticar("0000");

        Assert.That(sesion.Tarjeta!.EstaVigente(), Is.False);
    }

    [Test]
    public void Finalizar_DesdeValidando_PasaACerrando()
    {
        var sesion = new Sesion();
        sesion.IngresarTarjeta(TarjetaValida, s_futuro);

        sesion.Finalizar();

        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrando));
    }

    [Test]
    public void Sesion_RecorreLosEstadosDelDiagrama()
    {
        var sesion = new Sesion();
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Iniciada));

        sesion.IngresarTarjeta(TarjetaValida, s_futuro);
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Validando));

        sesion.Autenticar("1234");
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Operando));

        sesion.Ejecutar(sesion.NuevaConsulta());
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Operando));

        sesion.Finalizar();
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrando));

        sesion.Cerrar();
        Assert.That(sesion.Estado, Is.EqualTo(EstadoSesion.Cerrada));
    }

    [Test]
    public void Operaciones_SonPolimorficasPorEjecutar()
    {
        var sesion = SesionAutenticada();
        Operacion[] operaciones =
        [
            sesion.NuevaConsulta(),
            sesion.NuevaExtraccion(1_000m),
            sesion.NuevaTransferencia("0170099920000012345678", 500m),
        ];

        foreach (var operacion in operaciones)
        {
            Assert.That(sesion.Ejecutar(operacion), Is.True, operacion.Tipo);
        }
        Assert.That(sesion.Operaciones, Has.Count.EqualTo(3));
    }

    [Test]
    public void OperacionRechazada_NoQuedaEnElLog()
    {
        var sesion = SesionAutenticada();

        sesion.Ejecutar(sesion.NuevaExtraccion(200_000m));

        Assert.That(sesion.Operaciones, Is.Empty);
    }

    [TestCase(1000, "$1.000,00")]
    [TestCase(1234567.5, "$1.234.567,50")]
    [TestCase(0, "$0,00")]
    public void FormatoDinero_UsaSeparadoresArgentinos(double valor, string esperado)
    {
        Assert.That(Formato.Dinero((decimal)valor), Is.EqualTo(esperado));
    }
}
