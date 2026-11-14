using Cajero.Data;

namespace Cajero.Logic;

public class Transfer : Operation
{
    private string _destinationCbu;

    public Transfer(CentralDB central, Card card, Account account, string destinationCbu, decimal amount)
        : base(central, card, account, amount)
    {
        _destinationCbu = destinationCbu;
    }

    public string DestinationCbu
    {
        get => _destinationCbu;
        set => _destinationCbu = value;
    }

    public override string OperationType
    {
        get => "Transferencia";
    }

    public override bool Execute()
    {
        var response = _central.ExecuteTransfer(_card.Number, _destinationCbu, _amount);
        if (!response.Executed)
        {
            _message = response.Reason;
            return false;
        }
        _referenceNumber = response.ReceiptNumber;
        _account.UpdateBalance(_account.AvailableBalance - _amount, _account.TotalBalance - _amount);
        _message =
            $"Transferencia realizada\nDestino: {_destinationCbu}\n" +
            $"Monto: {MoneyFormatter.Format(_amount)}\nComprobante: {_referenceNumber}";
        return true;
    }
}
