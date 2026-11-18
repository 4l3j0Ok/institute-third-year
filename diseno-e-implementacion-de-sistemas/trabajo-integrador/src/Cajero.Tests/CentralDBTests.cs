using Cajero.Data;

namespace Cajero.Tests;

public class CentralDBTests
{
    private const string ValidCard = "1234567890123456";

    [Test]
    public void ValidatePin_AcceptsTheCorrectPin()
    {
        var central = new CentralDB();

        Assert.That(central.ValidarPin(ValidCard, "1234"), Is.True);
    }

    [Test]
    public void ValidatePin_RejectsTheWrongPin()
    {
        var central = new CentralDB();

        Assert.That(central.ValidarPin(ValidCard, "0000"), Is.False);
    }

    [Test]
    public void ValidatePin_RejectsAnUnknownCard()
    {
        var central = new CentralDB();

        Assert.That(central.ValidarPin("0000000000000000", "1234"), Is.False);
    }

    [Test]
    public void AuthorizeDebit_AssignsATransactionNumberWithTxPrefix()
    {
        var central = new CentralDB();

        var response = central.AutorizarDebito(ValidCard, 1_000m);

        Assert.That(response.Autorizado, Is.True);
        Assert.That(response.NumeroTransaccion, Does.Match("^TX[0-9]{8}$"));
    }

    [Test]
    public void ExecuteTransfer_AssignsAReceiptNumberWithTrPrefix()
    {
        var central = new CentralDB();

        var response = central.EjecutarTransferencia(ValidCard, "0170099920000012345678", 1_000m);

        Assert.That(response.Ejecutada, Is.True);
        Assert.That(response.NumeroComprobante, Does.Match("^TR[0-9]{8}$"));
    }
}
