using Cajero.Data;
using Cajero.Logic;

namespace Cajero.Tests;

public class CentralDBTests
{
    private const string ValidCard = "1234567890123456";

    [Test]
    public void ValidatePin_AcceptsTheEncryptedPin()
    {
        var central = new CentralDB();

        Assert.That(central.ValidatePin(ValidCard, Encryption.EncryptPin("1234")), Is.True);
    }

    [Test]
    public void ValidatePin_RejectsTheClearPin()
    {
        var central = new CentralDB();

        Assert.That(central.ValidatePin(ValidCard, "1234"), Is.False);
    }

    [Test]
    public void ValidatePin_RejectsAnUnknownCard()
    {
        var central = new CentralDB();

        Assert.That(central.ValidatePin("0000000000000000", Encryption.EncryptPin("1234")), Is.False);
    }

    [Test]
    public void AuthorizeDebit_AssignsATransactionNumberWithTxPrefix()
    {
        var central = new CentralDB();

        var response = central.AuthorizeDebit(ValidCard, 1_000m);

        Assert.That(response.Authorized, Is.True);
        Assert.That(response.TransactionNumber, Does.Match("^TX[0-9]{8}$"));
    }

    [Test]
    public void ExecuteTransfer_AssignsAReceiptNumberWithTrPrefix()
    {
        var central = new CentralDB();

        var response = central.ExecuteTransfer(ValidCard, "0170099920000012345678", 1_000m);

        Assert.That(response.Executed, Is.True);
        Assert.That(response.ReceiptNumber, Does.Match("^TR[0-9]{8}$"));
    }
}
