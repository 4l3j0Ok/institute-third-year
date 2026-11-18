using Cajero.Data;
using Cajero.Logic;

namespace Cajero.Tests;

public class SesionTests
{
    private const string ValidCard = "1234567890123456";
    private const string BlockedCard = "1111222233334444";
    private const string ExpiredCard = "5555666677778888";
    private static readonly DateTime s_future = new(2099, 12, 31);

    private static Sesion AuthenticatedSession()
    {
        var session = new Sesion();
        Assert.That(session.InsertarTarjeta(ValidCard, s_future), Is.True);
        Assert.That(session.Autenticar("1234"), Is.True);
        return session;
    }

    [Test]
    public void ValidCard_MovesToValidating()
    {
        var session = new Sesion();

        Assert.That(session.InsertarTarjeta(ValidCard, s_future), Is.True);
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Validando));
    }

    [Test]
    public void BlockedCard_ClosesTheSession()
    {
        var session = new Sesion();

        Assert.That(session.InsertarTarjeta(BlockedCard, s_future), Is.False);
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrando));
        Assert.That(session.Mensaje, Is.EqualTo("Tarjeta bloqueada"));
    }

    [Test]
    public void ExpiredCard_ClosesTheSession()
    {
        var session = new Sesion();

        Assert.That(session.InsertarTarjeta(ExpiredCard, new DateTime(2020, 1, 31)), Is.False);
        Assert.That(session.Mensaje, Is.EqualTo("Tarjeta vencida"));
    }

    [Test]
    public void CorrectPin_MovesToOperating()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Operando));
    }

    [Test]
    public void WrongPin_ReportsRemainingAttempts()
    {
        var session = new Sesion();
        session.InsertarTarjeta(ValidCard, s_future);

        Assert.That(session.Autenticar("0000"), Is.False);
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Validando));
        Assert.That(session.Mensaje, Does.Contain("Intentos restantes: 2"));
    }

    [Test]
    public void ThreeWrongPins_BlockTheCard()
    {
        var session = new Sesion();
        session.InsertarTarjeta(ValidCard, s_future);

        session.Autenticar("0000");
        session.Autenticar("0000");
        session.Autenticar("0000");

        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrando));
        Assert.That(session.Tarjeta!.Estado, Is.EqualTo(EstadoTarjeta.Bloqueada));
    }

    [Test]
    public void Authenticate_AllowsExactlyThreeAttempts()
    {
        var session = new Sesion();
        session.InsertarTarjeta(ValidCard, s_future);

        session.Autenticar("0000");
        session.Autenticar("0000");
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Validando));

        session.Autenticar("0000");
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrando));
        Assert.That(session.Tarjeta!.IntentosPin, Is.EqualTo(Tarjeta.IntentosMaximosPin));
    }

    [Test]
    public void CardBlockedByPin_IsNoLongerValid()
    {
        var session = new Sesion();
        session.InsertarTarjeta(ValidCard, s_future);
        session.Autenticar("0000");
        session.Autenticar("0000");
        session.Autenticar("0000");

        Assert.That(session.Tarjeta!.EsValida(), Is.False);
    }

    [Test]
    public void Finish_FromValidating_MovesToClosing()
    {
        var session = new Sesion();
        session.InsertarTarjeta(ValidCard, s_future);

        session.Finalizar();

        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrando));
    }

    [Test]
    public void Session_WalksThroughTheStatesOfTheDiagram()
    {
        var session = new Sesion();
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Iniciada));

        session.InsertarTarjeta(ValidCard, s_future);
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Validando));

        session.Autenticar("1234");
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Operando));

        session.Ejecutar(session.NuevaConsulta());
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Operando));

        session.Finalizar();
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrando));

        session.Cerrar();
        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrada));
    }

    [Test]
    public void CashWithdrawal_DeductsTheBalance()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Ejecutar(session.NuevaExtraccion(10_000m)), Is.True);
        Assert.That(session.Cuenta.SaldoDisponible, Is.EqualTo(140_000m));
        Assert.That(session.Operaciones, Has.Count.EqualTo(1));
    }

    [Test]
    public void CashWithdrawal_RejectsAmountAboveTheBalance()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Ejecutar(session.NuevaExtraccion(200_000m)), Is.False);
        Assert.That(session.Mensaje, Is.EqualTo("Saldo insuficiente"));
        Assert.That(session.Operaciones, Is.Empty);
    }

    [Test]
    public void CashWithdrawal_RejectsExceedingTheDailyLimit()
    {
        var session = AuthenticatedSession();
        session.Ejecutar(session.NuevaExtraccion(90_000m));

        Assert.That(session.Ejecutar(session.NuevaExtraccion(20_000m)), Is.False);
        Assert.That(session.Mensaje, Is.EqualTo("Supera el límite diario"));
    }

    [Test]
    public void Transfer_WithInvalidCbu_Fails()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Ejecutar(session.NuevaTransferencia("123", 1_000m)), Is.False);
        Assert.That(session.Mensaje, Is.EqualTo("CBU inválido"));
    }

    [Test]
    public void Transfer_Valid_DeductsTheBalanceAndGeneratesReceipt()
    {
        var session = AuthenticatedSession();
        var transfer = session.NuevaTransferencia("0170099920000012345678", 2_500m);

        Assert.That(session.Ejecutar(transfer), Is.True);
        Assert.That(session.Cuenta.SaldoDisponible, Is.EqualTo(147_500m));
        Assert.That(transfer.NumeroReferencia, Does.StartWith("TR"));
    }

    [Test]
    public void Operations_ArePolymorphicThroughExecute()
    {
        var session = AuthenticatedSession();
        Operacion[] operations =
        [
            session.NuevaConsulta(),
            session.NuevaExtraccion(1_000m),
            session.NuevaTransferencia("0170099920000012345678", 500m),
        ];

        foreach (var operation in operations)
        {
            Assert.That(session.Ejecutar(operation), Is.True, operation.TipoOperacion);
        }
        Assert.That(session.Operaciones, Has.Count.EqualTo(3));
    }

    [Test]
    public void RejectedOperation_IsNotLogged()
    {
        var session = AuthenticatedSession();

        session.Ejecutar(session.NuevaExtraccion(200_000m));

        Assert.That(session.Operaciones, Is.Empty);
    }

    [Test]
    public void Receipt_ListsTheCompletedOperations()
    {
        var session = AuthenticatedSession();
        session.Ejecutar(session.NuevaConsulta());
        session.Ejecutar(session.NuevaExtraccion(1_000m));

        var receipt = session.GenerarComprobante();

        Assert.That(receipt, Does.Contain("Consulta de saldo"));
        Assert.That(receipt, Does.Contain("Extracción $1.000,00"));
    }

    [Test]
    public void Close_ClearsCardAndOperations()
    {
        var session = AuthenticatedSession();
        session.Ejecutar(session.NuevaConsulta());

        session.Cerrar();

        Assert.That(session.Estado, Is.EqualTo(EstadoSesion.Cerrada));
        Assert.That(session.Tarjeta, Is.Null);
        Assert.That(session.Operaciones, Is.Empty);
    }

    [TestCase(1000, "$1.000,00")]
    [TestCase(1234567.5, "$1.234.567,50")]
    [TestCase(0, "$0,00")]
    public void MoneyFormatter_UsesArgentineSeparators(double value, string expected)
    {
        Assert.That(FormateadorMonedas.Formatear((decimal)value), Is.EqualTo(expected));
    }
}
