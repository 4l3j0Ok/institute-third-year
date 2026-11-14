using Atm.Data;
using Atm.Logic;

namespace Atm.Tests;

public class SessionTests
{
    private const string ValidCard = "1234567890123456";
    private const string BlockedCard = "1111222233334444";
    private const string ExpiredCard = "5555666677778888";
    private static readonly DateTime s_future = new(2099, 12, 31);

    private static Session AuthenticatedSession()
    {
        var session = new Session();
        Assert.That(session.InsertCard(ValidCard, s_future), Is.True);
        Assert.That(session.Authenticate("1234"), Is.True);
        return session;
    }

    [Test]
    public void ValidCard_MovesToValidating()
    {
        var session = new Session();

        Assert.That(session.InsertCard(ValidCard, s_future), Is.True);
        Assert.That(session.State, Is.EqualTo(SessionState.Validating));
    }

    [Test]
    public void BlockedCard_ClosesTheSession()
    {
        var session = new Session();

        Assert.That(session.InsertCard(BlockedCard, s_future), Is.False);
        Assert.That(session.State, Is.EqualTo(SessionState.Closing));
        Assert.That(session.Message, Is.EqualTo("Tarjeta bloqueada"));
    }

    [Test]
    public void ExpiredCard_ClosesTheSession()
    {
        var session = new Session();

        Assert.That(session.InsertCard(ExpiredCard, new DateTime(2020, 1, 31)), Is.False);
        Assert.That(session.Message, Is.EqualTo("Tarjeta vencida"));
    }

    [Test]
    public void CorrectPin_MovesToOperating()
    {
        var session = AuthenticatedSession();

        Assert.That(session.State, Is.EqualTo(SessionState.Operating));
    }

    [Test]
    public void WrongPin_ReportsRemainingAttempts()
    {
        var session = new Session();
        session.InsertCard(ValidCard, s_future);

        Assert.That(session.Authenticate("0000"), Is.False);
        Assert.That(session.State, Is.EqualTo(SessionState.Validating));
        Assert.That(session.Message, Does.Contain("Intentos restantes: 2"));
    }

    [Test]
    public void ThreeWrongPins_BlockTheCard()
    {
        var session = new Session();
        session.InsertCard(ValidCard, s_future);

        session.Authenticate("0000");
        session.Authenticate("0000");
        session.Authenticate("0000");

        Assert.That(session.State, Is.EqualTo(SessionState.Closing));
        Assert.That(session.Card!.Status, Is.EqualTo(CardStatus.Blocked));
    }

    [Test]
    public void Authenticate_AllowsExactlyThreeAttempts()
    {
        var session = new Session();
        session.InsertCard(ValidCard, s_future);

        session.Authenticate("0000");
        session.Authenticate("0000");
        Assert.That(session.State, Is.EqualTo(SessionState.Validating));

        session.Authenticate("0000");
        Assert.That(session.State, Is.EqualTo(SessionState.Closing));
        Assert.That(session.Card!.PinAttempts, Is.EqualTo(Card.MaximumPinAttempts));
    }

    [Test]
    public void CardBlockedByPin_IsNoLongerValid()
    {
        var session = new Session();
        session.InsertCard(ValidCard, s_future);
        session.Authenticate("0000");
        session.Authenticate("0000");
        session.Authenticate("0000");

        Assert.That(session.Card!.IsValid(), Is.False);
    }

    [Test]
    public void Finish_FromValidating_MovesToClosing()
    {
        var session = new Session();
        session.InsertCard(ValidCard, s_future);

        session.Finish();

        Assert.That(session.State, Is.EqualTo(SessionState.Closing));
    }

    [Test]
    public void Session_WalksThroughTheStatesOfTheDiagram()
    {
        var session = new Session();
        Assert.That(session.State, Is.EqualTo(SessionState.Started));

        session.InsertCard(ValidCard, s_future);
        Assert.That(session.State, Is.EqualTo(SessionState.Validating));

        session.Authenticate("1234");
        Assert.That(session.State, Is.EqualTo(SessionState.Operating));

        session.Execute(session.NewBalanceInquiry());
        Assert.That(session.State, Is.EqualTo(SessionState.Operating));

        session.Finish();
        Assert.That(session.State, Is.EqualTo(SessionState.Closing));

        session.Close();
        Assert.That(session.State, Is.EqualTo(SessionState.Closed));
    }

    [Test]
    public void CashWithdrawal_DeductsTheBalance()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Execute(session.NewCashWithdrawal(10_000m)), Is.True);
        Assert.That(session.Account.AvailableBalance, Is.EqualTo(140_000m));
        Assert.That(session.Operations, Has.Count.EqualTo(1));
    }

    [Test]
    public void CashWithdrawal_RejectsAmountAboveTheBalance()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Execute(session.NewCashWithdrawal(200_000m)), Is.False);
        Assert.That(session.Message, Is.EqualTo("Saldo insuficiente"));
        Assert.That(session.Operations, Is.Empty);
    }

    [Test]
    public void CashWithdrawal_RejectsExceedingTheDailyLimit()
    {
        var session = AuthenticatedSession();
        session.Execute(session.NewCashWithdrawal(90_000m));

        Assert.That(session.Execute(session.NewCashWithdrawal(20_000m)), Is.False);
        Assert.That(session.Message, Is.EqualTo("Supera el límite diario"));
    }

    [Test]
    public void Transfer_WithInvalidCbu_Fails()
    {
        var session = AuthenticatedSession();

        Assert.That(session.Execute(session.NewTransfer("123", 1_000m)), Is.False);
        Assert.That(session.Message, Is.EqualTo("CBU inválido"));
    }

    [Test]
    public void Transfer_Valid_DeductsTheBalanceAndGeneratesReceipt()
    {
        var session = AuthenticatedSession();
        var transfer = session.NewTransfer("0170099920000012345678", 2_500m);

        Assert.That(session.Execute(transfer), Is.True);
        Assert.That(session.Account.AvailableBalance, Is.EqualTo(147_500m));
        Assert.That(transfer.ReferenceNumber, Does.StartWith("TR"));
    }

    [Test]
    public void Operations_ArePolymorphicThroughExecute()
    {
        var session = AuthenticatedSession();
        Operation[] operations =
        [
            session.NewBalanceInquiry(),
            session.NewCashWithdrawal(1_000m),
            session.NewTransfer("0170099920000012345678", 500m),
        ];

        foreach (var operation in operations)
        {
            Assert.That(session.Execute(operation), Is.True, operation.OperationType);
        }
        Assert.That(session.Operations, Has.Count.EqualTo(3));
    }

    [Test]
    public void RejectedOperation_IsNotLogged()
    {
        var session = AuthenticatedSession();

        session.Execute(session.NewCashWithdrawal(200_000m));

        Assert.That(session.Operations, Is.Empty);
    }

    [Test]
    public void Receipt_ListsTheCompletedOperations()
    {
        var session = AuthenticatedSession();
        session.Execute(session.NewBalanceInquiry());
        session.Execute(session.NewCashWithdrawal(1_000m));

        var receipt = session.GenerateReceipt();

        Assert.That(receipt, Does.Contain("Consulta de saldo"));
        Assert.That(receipt, Does.Contain("Extracción $1.000,00"));
    }

    [Test]
    public void Close_ClearsCardAndOperations()
    {
        var session = AuthenticatedSession();
        session.Execute(session.NewBalanceInquiry());

        session.Close();

        Assert.That(session.State, Is.EqualTo(SessionState.Closed));
        Assert.That(session.Card, Is.Null);
        Assert.That(session.Operations, Is.Empty);
    }

    [TestCase(1000, "$1.000,00")]
    [TestCase(1234567.5, "$1.234.567,50")]
    [TestCase(0, "$0,00")]
    public void MoneyFormatter_UsesArgentineSeparators(double value, string expected)
    {
        Assert.That(MoneyFormatter.Format((decimal)value), Is.EqualTo(expected));
    }
}
