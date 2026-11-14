using Atm.Data;

namespace Atm.Logic;

public class CashWithdrawal : Operation
{
    public CashWithdrawal(CentralDB central, Card card, Account account, decimal amount)
        : base(central, card, account, amount)
    {
    }

    public override string OperationType
    {
        get => "Extracción";
    }

    public override bool Execute()
    {
        var response = _central.AuthorizeDebit(_card.Number, _amount);
        if (!response.Authorized)
        {
            _message = response.Reason;
            return false;
        }
        _referenceNumber = response.TransactionNumber;
        _account.UpdateBalance(response.RemainingBalance, response.RemainingBalance);
        _message =
            $"Operación exitosa\nEfectivo dispensado: {MoneyFormatter.Format(_amount)}\n" +
            $"Saldo restante: {MoneyFormatter.Format(response.RemainingBalance)}";
        return true;
    }
}
