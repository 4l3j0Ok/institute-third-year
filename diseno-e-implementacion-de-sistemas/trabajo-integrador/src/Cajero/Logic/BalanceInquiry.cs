using System;
using Cajero.Data;

namespace Cajero.Logic;

public class BalanceInquiry : Operation
{
    public BalanceInquiry(CentralDB central, Card card, Account account)
        : base(central, card, account, 0m)
    {
    }

    public override string OperationType
    {
        get => "Consulta de saldo";
    }

    public override bool Execute()
    {
        var response = _central.GetBalance(_card.Number);
        _account.UpdateBalance(response.Available, response.Total);
        _message =
            $"Saldo disponible: {MoneyFormatter.Format(response.Available)}\n" +
            $"Saldo total: {MoneyFormatter.Format(response.Total)}\n" +
            $"{DateTime.Now:dd/MM/yyyy HH:mm}";
        return true;
    }
}
